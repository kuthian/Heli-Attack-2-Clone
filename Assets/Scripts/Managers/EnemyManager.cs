using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public bool EnemiesEnabled = true;
    public GameObject pfEnemy;
    public Transform[] enemySpawnPoints;
    public Transform healthCrateSpawnPoint;
    internal List<GameObject> enemies;

    void Awake()
    {
        enemies = new List<GameObject>();
    }

    void Start()
    {
        if (EnemiesEnabled)
        {
            CreateEnemyRandom();
        }
    }

    public void End()
    {
        foreach (GameObject obj in enemies)
        {
            obj.GetComponent<EnemyController>().LeaveForever = true;
            obj.GetComponent<EnemyController>().GoToState(EnemyController.State.Leaving);
        }
    }

    private void OnEnable()
    {
        foreach (GameObject obj in enemies)
        {
            obj.GetComponent<Health>().OnHealthZero += HandleOnHealthZero;
        }
    }

    private void OnDisable()
    {
        foreach (GameObject obj in enemies)
        {
            if (obj)
            {
                obj.GetComponent<Health>().OnHealthZero -= HandleOnHealthZero;
            }
        }
    }

    private void CreateEnemyRandom()
    {
        CreateEnemy(Utils.RandomInRange(enemySpawnPoints));
    }

    private IEnumerator CreateEnemyRandomWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CreateEnemyRandom();
    }

    private void DropHealthCrate()
    {
        CrateGenerator.SpawnHealthCrate(healthCrateSpawnPoint.position);
    }

    private void CreateEnemy(Transform p)
    {
        GameObject obj = Instantiate(pfEnemy, p.position, Quaternion.identity, transform);
        obj.GetComponent<Health>().OnHealthZero += HandleOnHealthZero;
        //obj.GetComponent<EnemyController>().Init();
        obj.GetComponent<EnemyController>().IsFlipped = Utils.RandomBool();
        enemies.Insert(0, obj); // this keeps us from changing sorting order on remaining heli

        int sortingOrder = 4; // base sorting order for the heli
        for (int i = 0; i < enemies.Count; i++)
        {
            // Fix Z-fighting issue between multiple enemies
            enemies[i].transform.position = new Vector3(enemies[i].transform.position.x, enemies[i].transform.position.y, i);

            // Hack to prevent gunners from being sorted over other helicopters
            var gunner = enemies[i].transform.GetChild(0).GetChild(0);
            enemies[i].GetComponent<SpriteRenderer>().sortingOrder = sortingOrder++;
            gunner.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder++;
            gunner.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = sortingOrder++; // the gun!
            sortingOrder++; // trust me we need this

            // Enemy x tracking is out of phase so that they don't perform the exact same tracking behaviour
            enemies[i].GetComponent<EnemyController>().A0 = enemies.Count > 1 ? -2 + 4 * i : 0;
            enemies[i].GetComponent<EnemyController>().Phase = 90 * i;

            if (obj != enemies[i])
            {
                // Enemies shouldn't be able to interact
                Physics2D.IgnoreCollision(enemies[i].GetComponent<Collider2D>(), obj.GetComponent<Collider2D>());
            }
        }
    }

    void HandleOnHealthZero()
    {
        enemies.RemoveAll(HasHealthZero);
        HUDManager.ScoreCount.Add(1);
        StartCoroutine(CreateEnemyRandomWithDelay(1f));

        if (HUDManager.ScoreCount.Score % 10 == 0)
        {
            DropHealthCrate();
        }

        // Increase the difficulty when
        if (HUDManager.ScoreCount.Score == 1)
        {
            StartCoroutine(CreateEnemyRandomWithDelay(2f));
        }
    }

    private static bool HasHealthZero(GameObject obj)
    {
        return obj.GetComponent<Health>() &&
               obj.GetComponent<Health>().CurrentHealth <= 0;
    }
}
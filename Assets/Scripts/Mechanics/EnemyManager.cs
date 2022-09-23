using UnityEngine;

public class EnemyManager : MonoBehaviour {
  
  [SerializeField] private GameObject enemy;
  [SerializeField] private Transform player;
  [SerializeField] private HeliCountHUD heliCount;
  public Transform[] spawnPoints;

  void Start()
  {
    CreateEnemyRandom();
  }

  private void CreateEnemyRandom()
  {
    CreateEnemy( spawnPoints[ Random.Range(0, spawnPoints.Length) ] );
  }

  private void CreateEnemy( Transform p )
  {
    GameObject obj = Instantiate(enemy, p.position, Quaternion.identity);
    obj.GetComponent<EnemyController>().Init( player );
    obj.GetComponent<Health>().OnHealthZero += HandleOnHealthZero;
    if (Random.Range(0, 2) == 1) {
      // Flip the transform
      Quaternion r = transform.rotation;
      transform.rotation = new Quaternion(r.x, 180, r.z, r.w);
    }
  }

  void HandleOnHealthZero()
  {
    heliCount.Add(1);
    CreateEnemyRandom();
  }
}
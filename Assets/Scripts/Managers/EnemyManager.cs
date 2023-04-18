using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
  
  [SerializeField] private GameObject _pfEnemy;
  public Transform[] _spawnPoints;
  internal List<GameObject> _enemies;

  void Awake()
  {
    _enemies = new List<GameObject>();
  }

  void Start()
  {
    CreateEnemyRandom();
  }

  private void OnEnable()
  {
    foreach ( GameObject obj in _enemies )
    {
      obj.GetComponent<Health>().OnHealthZero += HandleOnHealthZero;
    }
  }

  private void OnDisable()
  {  
    foreach ( GameObject obj in _enemies )
    {
      if (obj) {
        obj.GetComponent<Health>().OnHealthZero -= HandleOnHealthZero;
      } 
    }
  }

  private void CreateEnemyRandom()
  {
    CreateEnemy( Utils.RandomInRange(_spawnPoints) );
  }

  private void CreateEnemy( Transform p )
  {
    GameObject obj = Instantiate(_pfEnemy, p.position, Quaternion.identity, transform);
    obj.GetComponent<Health>().OnHealthZero += HandleOnHealthZero;
    // TODO: Fix random enemy facing
    // if (Random.Range(0, 2) == 1) {
    //   // Flip the transform
    //   Quaternion r = transform.rotation;
    //   obj.transform.rotation = new Quaternion(r.x, 180, r.z, r.w);
    // }
    _enemies.Add( obj );
  }

  void HandleOnHealthZero()
  {
    _enemies.RemoveAll(HasHealthZero);
    HUDManager.ScoreCount.Add(1);
    CreateEnemyRandom();
  }

  private static bool HasHealthZero(GameObject obj) 
  {
      return obj.GetComponent<Health>() && 
             obj.GetComponent<Health>().CurrentHealth <= 0;
  }
}
using UnityEngine;

public class EnemyManager : MonoBehaviour {
  
  [SerializeField] private GameObject _pfEnemy;
  public Transform[] _spawnPoints;

  void Start()
  {
    CreateEnemyRandom();
  }

  private void CreateEnemyRandom()
  {
    CreateEnemy( Utils.RandomInRange(_spawnPoints) );
  }

  private void CreateEnemy( Transform p )
  {
    GameObject obj = Instantiate(_pfEnemy, p.position, Quaternion.identity);
    obj.GetComponent<Health>().OnHealthZero += HandleOnHealthZero;
    if (Random.Range(0, 2) == 1) {
      // Flip the transform
      Quaternion r = transform.rotation;
      transform.rotation = new Quaternion(r.x, 180, r.z, r.w);
    }
  }

  void HandleOnHealthZero()
  {
    HUDManager.HeliCount.Add(1);
    CreateEnemyRandom();
  }
}
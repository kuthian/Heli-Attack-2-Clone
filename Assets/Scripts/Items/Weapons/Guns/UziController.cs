using UnityEngine;

public class UziController : __GunController {

  [SerializeField] protected Transform _secondFirePointTransform;

  override protected void InstantiateProjectile()
  {
    {
      Transform t = _firePointTransform;
      Transform projectile = 
        Instantiate(_pfProjectile, t.position, Quaternion.identity, DynamicObjects.Projectiles);
      Vector3 direction = t.right - t.up * 0.001f * (float) Random.Range(-100,100);
      projectile.GetComponent<Projectile>().Setup(direction);
    }
    {
      Transform t = _secondFirePointTransform;
      Transform projectile = 
        Instantiate(_pfProjectile, t.position, Quaternion.identity, DynamicObjects.Projectiles);
      Vector3 direction = t.right - t.up * 0.001f * (float) Random.Range(-100,100);
      projectile.GetComponent<Projectile>().Setup(direction);
    }
  }

}
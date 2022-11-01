using UnityEngine;

public class RifleController : __GunController {

  override protected void InstantiateProjectile()
  {
    Transform projectileTransform = 
      Instantiate(_pfProjectile, _firePointTransform.position, Quaternion.identity, DynamicObjects.Projectiles);

    Vector3 direction = _firePointTransform.right;

    projectileTransform.GetComponent<Projectile>().Setup(direction);
  }

}
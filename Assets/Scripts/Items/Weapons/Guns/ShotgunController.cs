using UnityEngine;

public class ShotgunController : __GunController {

  [SerializeField] private float _pStepScalar = 0.1f;
  [SerializeField] private float _dStepScalar = 0.075f;

  override protected void InstantiateProjectile()
  {
    Vector3 pStep = _firePointTransform.up * _pStepScalar;
    Vector3 position = _firePointTransform.position - pStep * (_ammoPerShot / 2);
    Vector3 dStep = _firePointTransform.up * _dStepScalar;
    Vector3 direction = _firePointTransform.right - dStep * (_ammoPerShot / 2);;

    for (int i = 0; i < _ammoPerShot; i++)
    {
      Transform projectile = 
        Instantiate(_pfProjectile, position, Quaternion.identity, DynamicObjects.Projectiles);
      projectile.GetComponent<Projectile>().Setup(direction);
      position = position + pStep;
      direction = direction + dStep;
    }
  }

}
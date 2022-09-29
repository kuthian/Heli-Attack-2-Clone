using UnityEngine;

public class ShotgunController : GunController {

  [SerializeField] private float pStepScalar = 0.1f;
  [SerializeField] private float dStepScalar = 0.075f;

  override protected void InstantiateProjectile()
  {
    Vector3 pStep = firePointTransform.up * pStepScalar;
    Vector3 position = firePointTransform.position - pStep * (ammoPerShot / 2);
    Vector3 dStep = firePointTransform.up * dStepScalar;
    Vector3 direction = firePointTransform.right - dStep * (ammoPerShot / 2);;

    for (int i = 0; i < ammoPerShot; i++)
    {
      Transform projectile = 
        Instantiate(pfProjectile, position, Quaternion.identity, DynamicObjects.transform);
      projectile.GetComponent<Projectile>().Setup(direction);
      position = position + pStep;
      direction = direction + dStep;
    }
  }

}
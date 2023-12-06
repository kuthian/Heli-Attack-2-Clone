using UnityEngine;

public class ShotgunController : __GunController
{
    [SerializeField] private float pStepScalar = 0.1f;
    [SerializeField] private float dStepScalar = 0.075f;

    override protected void Shoot()
    {
        Vector3 pStep = firePointTransform.up * pStepScalar;
        Vector3 position = firePointTransform.position - pStep * (_ammoPerShot / 2);
        Vector3 dStep = firePointTransform.up * dStepScalar;
        Vector3 direction = firePointTransform.right - dStep * (_ammoPerShot / 2); ;

        for (int i = 0; i < _ammoPerShot; i++)
        {
            InstantiateProjectile(position, direction);
            position = position + pStep;
            direction = direction + dStep;
        }
        _wwOnShoot.Post(gameObject);
    }

}
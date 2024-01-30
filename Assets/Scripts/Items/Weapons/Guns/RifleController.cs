using UnityEngine;

public class RifleController : __GunController
{
    public ParticleSystem muzzleFire;

    override protected void Shoot()
    {
        muzzleFire.Play();
        base.Shoot();
    }

}
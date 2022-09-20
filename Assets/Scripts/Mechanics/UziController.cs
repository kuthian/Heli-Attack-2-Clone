using UnityEngine;

public class UziController : GunController {

  [SerializeField] protected Transform secondFirePointTransform;

  override protected void InstantiateProjectile()
  {
    {
      Transform t = firePointTransform;
      Transform projectile = 
        Instantiate(pfProjectile, t.position, Quaternion.identity);
      Vector3 direction = t.right - t.up * 0.001f * (float) Random.Range(-100,100);
      projectile.GetComponent<Projectile>().Setup(direction);
    }
    {
      Transform t = secondFirePointTransform;
      Transform projectile = 
        Instantiate(pfProjectile, t.position, Quaternion.identity);
      Vector3 direction = t.right - t.up * 0.001f * (float) Random.Range(-100,100);
      projectile.GetComponent<Projectile>().Setup(direction);
    }
  }

}
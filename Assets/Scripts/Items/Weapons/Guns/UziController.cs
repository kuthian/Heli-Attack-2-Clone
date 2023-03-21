using UnityEngine;

public class UziController : __GunController {

  [SerializeField] protected Transform _secondFirePointTransform;

  override protected void Shoot()
  {
    {
      Transform t = _firePointTransform;
      Vector3 direction = t.right - t.up * 0.001f * (float) Random.Range(-100,100);
      InstantiateProjectile( t.position, direction );
    }
    {
      Transform t = _secondFirePointTransform;
      Vector3 direction = t.right - t.up * 0.001f * (float) Random.Range(-100,100);
      InstantiateProjectile( t.position, direction );
    }
  }

}
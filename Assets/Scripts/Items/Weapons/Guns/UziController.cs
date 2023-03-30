using UnityEngine;

public class UziController : __GunController {

  [SerializeField]
  protected Transform _secondFirePointTransform;

  [SerializeField] private AK.Wwise.Event _wwShootStart;
  [SerializeField] private AK.Wwise.Event _wwShootEnd;
  [SerializeField] private AK.Wwise.Event _wwAmmoEmpty;

  override protected void OnShootStart()
  {
    _wwShootStart.Post(gameObject);
  }

  override protected void OnShootEnd()
  {
    _wwShootEnd.Post(gameObject);
  }

  override protected void OnAmmoEmpty()
  {
    _wwAmmoEmpty.Post(gameObject);
  }

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
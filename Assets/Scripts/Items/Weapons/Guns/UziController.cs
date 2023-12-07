using UnityEditor.U2D;
using UnityEngine;

public class UziController : __GunController
{
    [SerializeField]
    protected Transform secondFirePointTransform;

    [SerializeField]
    protected Animator gunfireAnimation;

    [SerializeField] private AK.Wwise.Event _wwShootStart;
    [SerializeField] private AK.Wwise.Event _wwShootEnd;
    [SerializeField] private AK.Wwise.Event _wwAmmoEmpty;

    override protected void OnShootStart()
    {
        _wwShootStart.Post(gameObject);
        gunfireAnimation.SetBool("isShooting", true);
    }

    override protected void OnShootEnd()
    {
        _wwShootEnd.Post(gameObject);
        gunfireAnimation.SetBool("isShooting", false);
        gunfireAnimation.GetComponent<SpriteRenderer>().sprite = null;
    }

    override protected void OnAmmoEmpty()
    {
        _wwAmmoEmpty.Post(gameObject);
        gunfireAnimation.SetBool("isShooting", false);
    }

    override protected void Shoot()
    {
        {
            Transform t = firePointTransform;
            Vector3 direction = t.right - t.up * 0.001f * (float)Random.Range(-100, 100);
            InstantiateProjectile(t.position, direction);
        }
        {
            Transform t = secondFirePointTransform;
            Vector3 direction = t.right - t.up * 0.001f * (float)Random.Range(-100, 100);
            InstantiateProjectile(t.position, direction);
        }
    }

}
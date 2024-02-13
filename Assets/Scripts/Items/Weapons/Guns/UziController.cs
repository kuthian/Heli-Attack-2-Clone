using UnityEngine;

public class UziController : __GunController
{
    [SerializeField]
    protected Transform secondFirePointTransform;

    [SerializeField]
    protected Animator gunfireAnimation;

    [SerializeField] private AK.Wwise.Event wwShootStart;
    [SerializeField] private AK.Wwise.Event wwShootEnd;
    [SerializeField] private AK.Wwise.Event wwAmmoEmpty;

    override protected void OnShootStart()
    {
        // Play the shooting sound
        wwShootStart.Post(gameObject);
        // Start the animation
        gunfireAnimation.SetBool("isShooting", true);
    }

    override protected void OnShootEnd()
    {
        // Stop the shooting sound
        wwShootEnd.Post(gameObject);
        // Stop the animation
        gunfireAnimation.SetBool("isShooting", false);
        // Reset the sprite in case the animation didn't finish
        gunfireAnimation.GetComponent<SpriteRenderer>().sprite = null; 
    }

    override protected void OnAmmoEmpty()
    {
        // stop the shooting sound
        wwAmmoEmpty.Post(gameObject);
        // Stop the animation
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
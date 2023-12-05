using System;
using System.Collections;
using UnityEngine;

public class EnemyGunController : MonoBehaviour
{
    [field: HideInInspector]
    public GameObject ObjectToIgnore { get; set; }

    public bool ShootingEnabled { get; set; } = true;

    [Serializable]
    public struct projectile
    {
        public Transform prefab;
        public float damage;
        public float speed;
        public float maxLifetime;
    }

    [SerializeField]
    private projectile pfProjectile;

    private DateTime lastShotTime = DateTime.Now;

    [SerializeField]
    private float cooldownTime = 2f;


    [field: HideInInspector]
    private Transform firePointTransform;

    private bool OnCoolDown => (lastShotTime).AddSeconds(cooldownTime) > DateTime.Now;

    private void Start()
    {
        firePointTransform = transform.Find("FirePoint");
    }

    private void ShootProjectile()
    {
        Transform projectile =
          Instantiate(pfProjectile.prefab, firePointTransform.position, Quaternion.identity, DynamicObjects.Projectiles);

        if (ObjectToIgnore)
        {
            var colliders = ObjectToIgnore.GetComponents<Collider2D>();
            foreach (Collider2D collider in colliders)
            {
                Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), collider);
            }
        }

        projectile.GetComponent<Projectile>().Damage = pfProjectile.damage;
        projectile.GetComponent<Projectile>().Speed = pfProjectile.speed;
        projectile.GetComponent<Projectile>().MaxLifetimeSeconds = pfProjectile.maxLifetime;
        projectile.GetComponent<Projectile>().Shoot(firePointTransform.right);
    }

    private void Update()
    {
        if (GameManager.Paused) return;

        if (ShootingEnabled && !OnCoolDown)
        {
            ShootProjectile();
            lastShotTime = DateTime.Now.AddSeconds(cooldownTime);
        }
    }

}
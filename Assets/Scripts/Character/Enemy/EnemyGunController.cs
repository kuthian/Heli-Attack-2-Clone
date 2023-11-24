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
    private projectile _projectile;

    [field: HideInInspector]
    private DateTime _lastShotTime = DateTime.Now;

    [SerializeField]
    private float _cooldownTime = 2f;


    [field: HideInInspector]
    private Transform _firePointTransform;

    private bool OnCoolDown => (_lastShotTime).AddSeconds(_cooldownTime) > DateTime.Now;

    private void Start()
    {
        _firePointTransform = transform.Find("FirePoint");
    }

    private void ShootProjectile()
    {
        Transform projectile =
          Instantiate(_projectile.prefab, _firePointTransform.position, Quaternion.identity, DynamicObjects.Projectiles);

        if (ObjectToIgnore)
        {
            var colliders = ObjectToIgnore.GetComponents<Collider2D>();
            foreach (Collider2D collider in colliders)
            {
                Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), collider);
            }
        }

        projectile.GetComponent<Projectile>().Damage = _projectile.damage;
        projectile.GetComponent<Projectile>().Speed = _projectile.speed;
        projectile.GetComponent<Projectile>().MaxLifetimeSeconds = _projectile.maxLifetime;
        projectile.GetComponent<Projectile>().Shoot(_firePointTransform.right);
    }

    private void Update()
    {
        if (GameManager.Paused) return;

        if (ShootingEnabled && !OnCoolDown)
        {
            ShootProjectile();
            _lastShotTime = DateTime.Now.AddSeconds(_cooldownTime);
        }
    }

}
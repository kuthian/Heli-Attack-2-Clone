using System;
using System.Collections;
using UnityEngine;

public class EnemyGunController : MonoBehaviour {

  [field:HideInInspector]
  public GameObject ObjectToIgnore { get; set; }

  public bool ShootingEnabled { get; set; } = true;

  [field:HideInInspector]
  private DateTime _lastShotTime = DateTime.Now;

  [SerializeField]
  private float _cooldownTime = 2f;

  [SerializeField]
  private GameObject _pfProjectile;

  [field:HideInInspector]
  private Transform _firePointTransform;

  private bool OnCoolDown => (_lastShotTime).AddSeconds(_cooldownTime) > DateTime.Now;

  private void Start()
  {
    _firePointTransform = transform.Find("FirePoint");
  }

  private void ShootProjectile()
  {
    GameObject projectile = 
      Instantiate(_pfProjectile, _firePointTransform.position, Quaternion.identity, DynamicObjects.Projectiles);

    if (ObjectToIgnore)
    {
      var colliders = ObjectToIgnore.GetComponents<Collider2D>();
      foreach ( Collider2D collider in colliders ) {
        Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), collider);
      }
    }

    Vector3 direction = _firePointTransform.right;

    projectile.GetComponent<Projectile>().Setup(direction);
  }

  private void Update()
  {
    if ( GameManager.Paused ) return;

    if ( ShootingEnabled && !OnCoolDown )
    {
      ShootProjectile();
      _lastShotTime = DateTime.Now.AddSeconds(_cooldownTime);
    }
  }

}
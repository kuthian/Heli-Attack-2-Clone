using System;
using System.Collections;
using UnityEngine;

public class EnemyGunController : MonoBehaviour {

  [HideInInspector] public GameObject objectToIgnore;

  [SerializeField] private GameObject _pfProjectile;
  private Transform _firePointTransform;

  [SerializeField] private float _cooldownTime = 2f;
  private DateTime _cooldownOffTime;

  private void Start()
  {
    _firePointTransform = transform.Find("FirePoint");
    _cooldownOffTime = DateTime.Now.AddSeconds(2);
  }

  private void ShootProjectile()
  {
    GameObject projectile = 
      Instantiate(_pfProjectile, _firePointTransform.position, Quaternion.identity, DynamicObjects.Projectiles);

    if (objectToIgnore)
    {
      var colliders = objectToIgnore.GetComponents<Collider2D>();
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

    var now = DateTime.Now;
    if (_cooldownOffTime <= now)
    {
      ShootProjectile();
      _cooldownOffTime = now.AddSeconds(_cooldownTime);
    }
  }

}
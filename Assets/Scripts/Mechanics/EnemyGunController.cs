using System;
using System.Collections;
using UnityEngine;

public class EnemyGunController : MonoBehaviour {

  [SerializeField] private Transform firePointTransform;
  [SerializeField] private GameObject pfProjectile;
  public GameObject objectToIgnore;

  [SerializeField] private float cooldownTime = 2f;
  private DateTime cooldownOffTime;

  private void Start()
  {
    cooldownOffTime = DateTime.Now.AddSeconds(2);
  }

  private void ShootProjectile()
  {
    GameObject projectile = 
      Instantiate(pfProjectile, firePointTransform.position, Quaternion.identity, DynamicObjects.transform);

    if (objectToIgnore)
    {
      var colliders = objectToIgnore.GetComponents<Collider2D>();
      foreach ( Collider2D collider in colliders ) {
        Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), collider);
      }
    }

    Vector3 direction = firePointTransform.right;

    projectile.GetComponent<Projectile>().Setup(direction);
  }

  private void Update()
  {
    var now = DateTime.Now;
    if (cooldownOffTime <= now)
    {
      ShootProjectile();
      cooldownOffTime = now.AddSeconds(cooldownTime);
    }
  }

}
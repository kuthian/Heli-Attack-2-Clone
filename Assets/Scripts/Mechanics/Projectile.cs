using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

  internal Rigidbody2D _rb;
  [SerializeField] private int _damage = 10;
  [SerializeField] private float _speed = 10f;
  [SerializeField] private float _maxLifetimeSeconds = 5f;

  private void Awake()
  {
    _rb = GetComponent<Rigidbody2D>();
  }

  public void Setup( Vector3 direction )
  {
    _rb.velocity = direction.normalized * _speed;
    transform.eulerAngles = new Vector3(0, 0, Utils.GetAngleFromVectorFloat(direction));
    Destroy(gameObject, _maxLifetimeSeconds);
  }

  void OnTriggerEnter2D (Collider2D other)
  {
    if (other.CompareTag("Map"))
    {
      Destroy(gameObject);
    }
    if (other.CompareTag("Enemy")) 
    {
      Destroy(gameObject);
    }
    if (CompareTag("EnemyProjectile") && other.CompareTag("Player")) 
    {
      // Enemy projectile hits Player
      other.gameObject.SendMessage("Damage", _damage);
      Destroy(gameObject);
    }
    if (CompareTag("PlayerProjectile") && other.CompareTag("Enemy")) 
    {
      // Player projectile hits Enemy
      other.gameObject.SendMessage("Damage", _damage);
      Destroy(gameObject);
    }
  }

  void OnTriggerExit2D (Collider2D other)
  {
    if (other.CompareTag("GameArea"))
    {
      Destroy(gameObject);
    }
  }
}

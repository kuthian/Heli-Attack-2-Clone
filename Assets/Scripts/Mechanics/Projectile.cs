using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

  internal Rigidbody2D _rb;
  [field:HideInInspector]
  public float Damage { get; set; } = 10;
  [field:HideInInspector]
  public float Speed { get; set; } = 10;
  [field:HideInInspector]
  public float MaxLifetimeSeconds { get; set; } = 5;

  [SerializeField]
  private AK.Wwise.Event _wwOnImpact;

  [SerializeField]
  private AK.Wwise.Event _wwOnLeaveGameArea;

  private void Awake()
  {
    _rb = GetComponent<Rigidbody2D>();
  }

  public void Shoot( Vector3 direction )
  {
    _rb.velocity = direction.normalized * Speed;
    transform.eulerAngles = new Vector3(0, 0, Utils.GetAngleFromVectorFloat(direction));
    Destroy(gameObject, MaxLifetimeSeconds);
  }

  private void PlayImpactSound()
  {
    if (_wwOnImpact.IsValid()) {
      _wwOnImpact.Post(gameObject);
    }
  }

  void OnTriggerEnter2D (Collider2D other)
  {
    if (other.CompareTag("Map"))
    {
      // Any Projectile hits a map object
      PlayImpactSound();
      Destroy(gameObject);
    }
    else 
    if (CompareTag("EnemyProjectile") && other.CompareTag("Player")) 
    {
      // Enemy projectile hits Player
      PlayImpactSound();
      other.gameObject.SendMessage("Damage", Damage);
      Destroy(gameObject);
    }
    else 
    if (CompareTag("PlayerProjectile") && other.CompareTag("Enemy")) 
    {
      // Player projectile hits Enemy
      PlayImpactSound();
      other.gameObject.SendMessage("Damage", Damage);
      Destroy(gameObject);
    }
  }

  void OnTriggerExit2D (Collider2D other)
  {
    if (other.CompareTag("GameArea"))
    {
      if (_wwOnLeaveGameArea.IsValid()) {
        _wwOnLeaveGameArea.Post(gameObject);
      }
      Destroy(gameObject);
    }
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

  internal Rigidbody2D rb;
  [field:HideInInspector]
  public float Damage { get; set; } = 10;
  [field:HideInInspector]
  public float Speed { get; set; } = 10;
  [field:HideInInspector]
  public float MaxLifetimeSeconds { get; set; } = 5;

  [SerializeField]
  private AK.Wwise.Event _wwOnImpactPlayer;

  [SerializeField]
  private AK.Wwise.Event _wwOnImpactEnemy;

  [SerializeField]
  private AK.Wwise.Event _wwOnImpactMap;

  [SerializeField]
  private AK.Wwise.Event _wwOnLeaveGameArea;

  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  public void Shoot( Vector3 direction )
  {
    rb.velocity = direction.normalized * Speed;
    transform.eulerAngles = new Vector3(0, 0, Utils.GetAngleFromVectorFloat(direction));
    Destroy(gameObject, MaxLifetimeSeconds);
  }

  private void wwPostEvent( AK.Wwise.Event ww_event )
  {
    if (ww_event.IsValid()) {
      ww_event.Post(gameObject);
    }
  }

  void OnTriggerEnter2D (Collider2D other)
  {
    if (other.CompareTag("Map"))
    {
      // Any Projectile hits a map object
      wwPostEvent( _wwOnImpactMap );
      Destroy(gameObject);
    }
    else 
    if (CompareTag("EnemyProjectile") && other.CompareTag("Player")) 
    {
      // Enemy projectile hits Player
      wwPostEvent( _wwOnImpactPlayer );
      other.gameObject.SendMessage("Damage", Damage);
      Destroy(gameObject);
    }
    else 
    if (CompareTag("PlayerProjectile") && other.CompareTag("Enemy")) 
    {
      // Player projectile hits Enemy
      wwPostEvent( _wwOnImpactEnemy );
      other.gameObject.SendMessage("Damage", Damage);
      StatsManager.RegisterBulletHit(); // TODO: Move this elsewhere?
      Destroy(gameObject);
    }
  }

  void OnTriggerExit2D (Collider2D other)
  {
    if (other.CompareTag("GameArea"))
    {
      wwPostEvent( _wwOnLeaveGameArea );
      Destroy(gameObject);
    }
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  [SerializeField] private Rigidbody2D rb;
  public float speed = 10f;
  public float maxLifetimeSeconds = 5f;

  public void Setup( Vector3 direction )
  {
    rb.velocity = direction.normalized * speed;
    transform.eulerAngles = new Vector3(0, 0, Utils.UtilsClass.GetAngleFromVectorFloat(direction));
    Destroy(gameObject, maxLifetimeSeconds);
  }

  void OnTriggerEnter2D (Collider2D other)
  {
    if (other.CompareTag("Map"))
    {
      Destroy(gameObject);
    }
    if (other.CompareTag("Enemy")) 
    {
      Destroy(other.gameObject);
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

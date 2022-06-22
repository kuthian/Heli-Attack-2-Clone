using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  [SerializeField] private Rigidbody2D rb;
  private Vector3 direction;
  private float speed = 10f;

  public void Setup( Vector3 direction )
  {
    this.direction = direction;
    rb.velocity = direction.normalized * speed;
    Destroy(gameObject, 5.0f);
  }

  private void Update()
  {
  }

  void OnTriggerEnter2D (Collider2D other)
  {
    if (other.gameObject.tag == "Map")
    {
      Destroy(gameObject);
    }
    if (other.gameObject.tag == "Enemy") 
    {
      Destroy(other.gameObject);
      Destroy(gameObject);
    }
  }

  void OnTriggerExit2D (Collider2D other)
  {
    if (other.gameObject.tag == "GameArea")
    {
      Destroy(gameObject);
    }
  }
}

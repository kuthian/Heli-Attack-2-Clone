using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour {

  internal Rigidbody2D rb;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  void OnTriggerEnter2D (Collider2D other)
  {
    if (other.CompareTag("Map"))
    {
      rb.angularVelocity = 0;
      rb.constraints = RigidbodyConstraints2D.FreezePosition;
      ParticleManager.PlayExplodedEffect( transform );
      Destroy(gameObject, 1.5f);
    }
  }


}
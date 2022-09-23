using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour {

  internal Rigidbody2D rb;
  [SerializeField] private ParticleSystem pfExplodeEffect;

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
      ParticleSystem obj = Instantiate(pfExplodeEffect, transform);
      Destroy(gameObject, 0.75f);
    }
  }


}
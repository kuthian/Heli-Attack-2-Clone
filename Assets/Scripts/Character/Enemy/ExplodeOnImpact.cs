using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour {

  internal Rigidbody2D _rb;

  void Start()
  {
    _rb = GetComponent<Rigidbody2D>();
  }

  void OnTriggerEnter2D (Collider2D other)
  {
    if (other.CompareTag("Map"))
    {
      _rb.angularVelocity = 0;
      _rb.constraints = RigidbodyConstraints2D.FreezePosition;
      ParticleManager.PlayExplodedEffect( transform );
      AudioManager.PlayExplosion();
      Destroy(gameObject, 0.75f);
    }
  }


}
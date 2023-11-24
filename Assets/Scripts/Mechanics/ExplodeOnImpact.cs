using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour
{
    internal Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Map"))
        {
            ParticleManager.PlayExplodedHeliEffect(transform);
            Destroy(gameObject);
        }
    }


}
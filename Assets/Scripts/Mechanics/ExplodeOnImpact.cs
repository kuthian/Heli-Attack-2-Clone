using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour
{
    internal Rigidbody2D rb;
    public float cameraShakeAmplitude = 1;
    public float cameraShakeDurationSeconds = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Map"))
        {
            ParticleManager.PlayExplodedHeliEffect(transform);
            CameraShaker.ShakeCamera(cameraShakeAmplitude, cameraShakeDurationSeconds);
            Destroy(gameObject);
        }
    }


}
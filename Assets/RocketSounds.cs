using UnityEngine;

public class RocketSounds : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event rocketShoot;
    [SerializeField] private AK.Wwise.Event rocketImpact;

    void Start()
    {
        rocketShoot.Post(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Map"))
        {
            rocketImpact.Post(gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            rocketImpact.Post(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BulletLimit"))
        {
            rocketImpact.Post(gameObject);
        }
    }







}

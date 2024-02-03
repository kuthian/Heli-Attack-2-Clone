using System.Collections;
using UnityEngine;

public class EnemyLifecycle : MonoBehaviour
{
    [SerializeField]
    private GameObject pfDestroyedHelicopter;

    [SerializeField]
    private GameObject pfDestroyedGunner;

    [Range(0, 1)]
    public float crateDropRate = 0.5f;

    internal Rigidbody2D rb;
    internal SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        GetComponent<Health>().OnHealthZero += HandleOnHealthZero;
        GetComponent<Health>().OnHealthChanged += HandleOnHealthChanged;
    }

    private void OnDisable()
    {
        GetComponent<Health>().OnHealthZero -= HandleOnHealthZero;
        GetComponent<Health>().OnHealthChanged -= HandleOnHealthChanged;
    }

    private void HandleOnHealthZero()
    {
        GameObject destroyedGunner = Instantiate(pfDestroyedGunner, transform.position, transform.rotation);
        destroyedGunner.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 3.0f, 0.0f);
        destroyedGunner.GetComponent<Rigidbody2D>().angularVelocity = 20.0f;
        Destroy(destroyedGunner, 2);

        GameObject destroyedHeli = Instantiate(pfDestroyedHelicopter, transform.position, transform.rotation);
        destroyedHeli.GetComponent<Rigidbody2D>().velocity = rb.velocity;
        int direction = rb.velocity.x >= 0 ? -1 : 1;
        destroyedHeli.GetComponent<Rigidbody2D>().angularVelocity = direction * 11.0f;

        ParticleManager.PlayExplodedEffect(destroyedHeli.transform);

        if (Utils.Chance((int)(crateDropRate * 100)))
        {
            CrateGenerator.SpawnWeaponCrateRandom(transform.position);
        }

        Destroy(gameObject);
    }

    private IEnumerator FlashWhite()
    {
        spriteRenderer.color = new Color(0.75f, 0.75f, 0.75f, 1.0f);
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = Color.white;
    }

    private void HandleOnHealthChanged(int health)
    {
        StartCoroutine(FlashWhite());
    }

}
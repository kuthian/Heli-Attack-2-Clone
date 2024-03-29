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
    internal EnemyHealthBar healthBar;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar = GetComponent<EnemyHealthBar>();
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
        destroyedGunner.GetComponent<SpriteRenderer>().sortingOrder = spriteRenderer.sortingOrder + 1;
        Destroy(destroyedGunner, 2);

        GameObject destroyedHeli = Instantiate(pfDestroyedHelicopter, transform.position, transform.rotation);
        destroyedHeli.GetComponent<SpriteRenderer>().sortingOrder = spriteRenderer.sortingOrder;
        ParticleSystemRenderer[] renderers = destroyedHeli.GetComponentsInChildren<ParticleSystemRenderer>();
        renderers[0].sortingOrder = spriteRenderer.sortingOrder + 1; // explode particle effect
        renderers[1].sortingOrder = spriteRenderer.sortingOrder - 1; // smoke particle effect
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
        healthBar.SetPercentFill(health);
        StartCoroutine(FlashWhite());
    }

}
using System.Collections;
using UnityEngine;

public class EnemyLifecycle : MonoBehaviour {
  
  [SerializeField]
  private GameObject _pfDestroyedHelicopter;
  
  [SerializeField]
  private GameObject _pfDestroyedGunner;

  internal Rigidbody2D _rb;
  internal SpriteRenderer _spriteRenderer;

  void Awake()
  {
    _rb = GetComponent<Rigidbody2D>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
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
    GameObject destroyedGunner = Instantiate(_pfDestroyedGunner, transform.position, transform.rotation);
    destroyedGunner.GetComponent<Rigidbody2D>().velocity = new Vector3( 0.0f, 3.0f, 0.0f );
    destroyedGunner.GetComponent<Rigidbody2D>().angularVelocity = 20.0f;
    Destroy(destroyedGunner, 2);

    GameObject destroyedHeli = Instantiate(_pfDestroyedHelicopter, transform.position, transform.rotation);
    destroyedHeli.GetComponent<Rigidbody2D>().velocity = _rb.velocity;
    int direction = _rb.velocity.x >= 0 ? -1 : 1;
    destroyedHeli.GetComponent<Rigidbody2D>().angularVelocity = direction * 11.0f;

    ParticleManager.PlayExplodedEffect( destroyedHeli.transform );

    CrateGenerator.SpawnCrateRandom( transform.position );

    Destroy(gameObject);
  }

  private IEnumerator FlashWhite()
  {
    _spriteRenderer.color = new Color( 0.75f, 0.75f, 0.75f, 1.0f  );
    yield return new WaitForSeconds(0.05f);
    _spriteRenderer.color = Color.white;
  }

  private void HandleOnHealthChanged(int health)
  {
    StartCoroutine(FlashWhite());
  }

}
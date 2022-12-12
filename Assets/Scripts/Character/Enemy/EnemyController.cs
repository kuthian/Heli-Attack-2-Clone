using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour {
  
  [SerializeField] private float _maxSpeed = 3f;
  [SerializeField] private float _MoveAcceleration = 5;
  [SerializeField] private float _MoveDecceleration = 5;
  [SerializeField] private float _tolerance = 2f;
  [SerializeField] private GameObject _pfDestroyedHelicopter;
  [SerializeField] private GameObject _pfDestroyedGunner;

  private int _horizontal = 0;
  private Transform _target;

  internal Rigidbody2D _rb;
  internal SpriteRenderer _spriteRenderer;

  void Awake()
  {
    _rb = GetComponent<Rigidbody2D>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _target = GameObject.Find("Player").transform;
    GetComponentInChildren<AimAtTransform>().target = _target;
    GetComponentInChildren<EnemyGunController>().objectToIgnore = gameObject;
    GetComponent<Health>().OnHealthZero += HandleOnHealthZero;
    GetComponent<Health>().OnHealthChanged += HandleOnHealthChanged;
  }

  void Update()
  {
    float xDistance = transform.position.x - _target.position.x;  
    if (Mathf.Abs(xDistance) < _tolerance) _horizontal = 0;
    else if (xDistance > 0) _horizontal = -1;
    else if (xDistance < 0) _horizontal = 1;
  }

  void FixedUpdate()
  {
    if ( _horizontal != 0 )
    {
      float speed = Mathf.Abs(_rb.velocity.x) + _MoveAcceleration * Time.fixedDeltaTime;
      if (speed > _maxSpeed) speed = _maxSpeed;
      _rb.velocity = new Vector2(_horizontal * speed, _rb.velocity.y);
    }
    else
    {
      int direction = _rb.velocity.x > 0 ? 1 : -1;
      float speed = Mathf.Abs(_rb.velocity.x) - _MoveDecceleration * Time.fixedDeltaTime;
      if (speed < 0) speed = 0;
      _rb.velocity = new Vector2(direction * speed, _rb.velocity.y);
    }

    Quaternion r = transform.rotation;
    r.eulerAngles = new Vector3(r.eulerAngles.x, r.eulerAngles.y, -5.0f * _rb.velocity.x);
    transform.rotation = new Quaternion(r.x, r.y, r.z, r.w);
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
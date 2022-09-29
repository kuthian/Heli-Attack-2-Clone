using UnityEngine;

public class EnemyController : MonoBehaviour {
  
  [SerializeField] private float maxSpeed = 3f;
  [SerializeField] private float MoveAcceleration = 5;
  [SerializeField] private float MoveDecceleration = 5;
  [SerializeField] private float tolerance = 2f;
  [SerializeField] private Transform target;

  [SerializeField] private GameObject pfDestroyedHelicopter;
  [SerializeField] private GameObject pfDestroyedGunner;
  [SerializeField] private GameObject[] pfWeaponCrates; 

  internal Rigidbody2D rb;

  private int horizontal = 0;

  void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    target = GameObject.Find("Player").transform;
    GetComponentInChildren<AimAtTransform>().target = target;
    GetComponentInChildren<EnemyGunController>().objectToIgnore = gameObject;
    GetComponent<Health>().OnHealthZero += HandleOnHealthZero;
  }

  void Update()
  {
    float xDistance = transform.position.x - target.position.x;  
    if (Mathf.Abs(xDistance) < tolerance) horizontal = 0;
    else if (xDistance > 0) horizontal = -1;
    else if (xDistance < 0) horizontal = 1;
  }

  void FixedUpdate()
  {
    if ( horizontal != 0 )
    {
      float speed = Mathf.Abs(rb.velocity.x) + MoveAcceleration * Time.fixedDeltaTime;
      if (speed > maxSpeed) speed = maxSpeed;
      rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    else
    {
      int direction = rb.velocity.x > 0 ? 1 : -1;
      float speed = Mathf.Abs(rb.velocity.x) - MoveDecceleration * Time.fixedDeltaTime;
      if (speed < 0) speed = 0;
      rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    Quaternion r = transform.rotation;
    r.eulerAngles = new Vector3(r.eulerAngles.x, r.eulerAngles.y, -5.0f * rb.velocity.x);
    transform.rotation = new Quaternion(r.x, r.y, r.z, r.w);
  }

  private void HandleOnHealthZero()
  {
    GameObject destroyedGunner = Instantiate(pfDestroyedGunner, transform.position, transform.rotation);
    destroyedGunner.GetComponent<Rigidbody2D>().velocity = new Vector3( 0.0f, 3.0f, 0.0f );
    destroyedGunner.GetComponent<Rigidbody2D>().angularVelocity = 20.0f;
    Destroy(destroyedGunner, 2);

    GameObject destroyedHeli = Instantiate(pfDestroyedHelicopter, transform.position, transform.rotation);
    destroyedHeli.GetComponent<Rigidbody2D>().velocity = rb.velocity;
    int direction = rb.velocity.x >= 0 ? -1 : 1;
    destroyedHeli.GetComponent<Rigidbody2D>().angularVelocity = direction * 11.0f;

    ParticleManager.PlayExplodedEffect( destroyedHeli.transform );

    AudioManager.PlayExplosion();

    Instantiate(pfWeaponCrates[Random.Range(0, pfWeaponCrates.Length)], transform.position, Quaternion.identity);

    Destroy(gameObject);
  }

}
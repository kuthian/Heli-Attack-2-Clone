using UnityEngine;

public class EnemyController : MonoBehaviour {
  
  public float maxSpeed = 3f;
  public float MoveAcceleration = 5;
  public float MoveDecceleration = 5;

  internal Rigidbody2D rb;
  private Transform target;
  public float tolerance = 2f;
  public float vel;

  public GameObject[] pfWeaponCrates; 

  int horizontal = 0;

  public void Init( Transform t )
  {
    target = t;
    GetComponentInChildren<AimAtTransform>().target = t;
  }

  void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
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
    vel = rb.velocity.x;
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
    Instantiate(pfWeaponCrates[Random.Range(0, pfWeaponCrates.Length)], transform.position, Quaternion.identity);
    Destroy(gameObject);
  }

}
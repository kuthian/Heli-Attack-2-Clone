using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

  public bool BlockInput = false;

  private InventoryController _inventory;
  private Transform _groundCheck;
  private LayerMask _groundLayer;

  internal Rigidbody2D _rb;
  internal Collider2D _headCollider;

  private float _maxSpeed = 3f;
  private float _jumpingPower = 8f;
  private float _jumpingDeceleration = 0.5f;
  private float _MoveAcceleration = 20;
  private float _MoveDecceleration = 12;

  private bool _pauseGroundCheck = false;
  private bool _jump = false;
  private bool _stopJump = false;
  private bool _jumping = false;
  private int _jumpCounter = 2;
  private int _maxJumpCount = 2;

  public float InputX { get; private set; }
  public bool Crouched { get; private set; }
  public bool Grounded { get; private set; }
  
  public Rigidbody2D Rigidbody => _rb;
  public int JumpCount => _maxJumpCount - _jumpCounter;

  public float VelocityX => _rb.velocity.x;
  public float VelocityY => _rb.velocity.y;
  public float SpeedX => Mathf.Abs(VelocityX);
  public float SpeedY => Mathf.Abs(VelocityY);
  public float DirectionX => Mathf.Sign(VelocityX);

  private void Awake()
  {
    _headCollider = GetComponent<CircleCollider2D>();
    _rb = GetComponent<Rigidbody2D>();
    _inventory = GetComponentInChildren<InventoryController>();
    _groundCheck = transform.Find("GroundCheck");
    _groundLayer = LayerMask.GetMask("Ground");
  }

  private void Update()
  {
    if (GameManager.Paused || BlockInput) return;

    InputX = Input.GetAxisRaw("Horizontal");
    Crouched = Input.GetAxisRaw("Vertical") == -1;

    _headCollider.enabled = !Crouched;

    if (!_pauseGroundCheck)
    {
      Grounded = IsGrounded();
    }

    if (Grounded)
    {
      _jumping = false;
      _jumpCounter = _maxJumpCount;
      if (Crouched) InputX = 0;
    }
    
    if (Input.GetButtonDown("Jump") && _jumpCounter > 0 && !Crouched)
    {
      _jump = true;
      Grounded = false;
      StartCoroutine(PauseGroundCheck(0.4f));
    }

    if (Input.GetButtonUp("Jump") && _jumping && VelocityY > 0f)
    {
      _stopJump = true;
    }
  }

  private void FixedUpdate()
  {
    if (InputX != 0)
    {
      float speed = SpeedX + _MoveAcceleration * Time.fixedDeltaTime;
      if (speed > _maxSpeed) speed = _maxSpeed;
      _rb.velocity = new Vector2(InputX * speed, VelocityY);
    }
    else
    {
      float speed = SpeedX - _MoveDecceleration * Time.fixedDeltaTime;
      if (speed < 0) speed = 0;
      _rb.velocity = new Vector2(DirectionX * speed, VelocityY);
    }

    if (_jump)
    {
      _rb.velocity = new Vector2(VelocityX, _jumpingPower); 
      _jumpCounter--;
      _jumping = true;
      _jump = false;
    }
    if (_stopJump)
    {
      _rb.velocity = new Vector2(VelocityX, VelocityY*_jumpingDeceleration); 
      _stopJump = false;
    }

    // _inventory.GunController().Steady = SpeedX != 0.0f || SpeedY != 0.0f;

  }

  private bool IsGrounded()
  {
    return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
  }

  private IEnumerator PauseGroundCheck(float pauseTime)
  {
    _pauseGroundCheck = true;
    yield return new WaitForSeconds(pauseTime);
    _pauseGroundCheck = false;
  }

  public void AddWeapon( GameObject weapon )
  {
    _inventory.AddWeapon( weapon );
  }

  public void Damage( int var )
  {
    ParticleManager.PlayDamagedPlayerEffect( transform );
  }

}
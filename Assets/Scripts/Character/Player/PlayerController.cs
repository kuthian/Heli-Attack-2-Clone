using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

  private Transform _inventory;
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

  private float _horizontal;
  private bool _crouched;
  private bool _grounded;
  
  public bool Crouched => _crouched;
  public bool Grounded => _grounded;
  public int JumpCount => _maxJumpCount - _jumpCounter;
  public float SpeedX => Mathf.Abs(_rb.velocity.x);

  private void Awake()
  {
    _headCollider = GetComponent<CircleCollider2D>();
    _rb = GetComponent<Rigidbody2D>();
    _inventory = transform.Find("Inventory");
    _groundCheck = transform.Find("GroundCheck");
    _groundLayer = LayerMask.GetMask("Ground");
  }

  private void Update()
  {
    if (GameManager.Paused) return;

    _horizontal = Input.GetAxisRaw("Horizontal");
    _crouched = Input.GetAxisRaw("Vertical") == -1;

    _headCollider.enabled = !_crouched;

    if (!_pauseGroundCheck)
    {
      _grounded = IsGrounded();
    }

    if (_grounded)
    {
      _jumping = false;
      _jumpCounter = _maxJumpCount;
      if (_crouched) _horizontal = 0;
    }
    
    if (Input.GetButtonDown("Jump") && _jumpCounter > 0 && !_crouched)
    {
      _jump = true;
      _grounded = false;
      StartCoroutine(PauseGroundCheck(0.4f));
    }

    if (Input.GetButtonUp("Jump") && _jumping && _rb.velocity.y > 0f)
    {
      _stopJump = true;
    }
  }

  private void FixedUpdate()
  {
    if (_horizontal != 0)
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

    if (_jump)
    {
      _rb.velocity = new Vector2(_rb.velocity.x, _jumpingPower); 
      _jumpCounter--;
      _jumping = true;
      _jump = false;
    }
    if (_stopJump)
    {
      _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y*_jumpingDeceleration); 
      _stopJump = false;
    }
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
    _inventory.GetComponent<InventoryController>().AddWeapon( weapon );
  }

  public void Damage( int var )
  {
    ParticleManager.PlayDamagedPlayerEffect( transform );
  }

  public void PlayFmodFootstepsEvent()
  {
    FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Footsteps", GetComponent<Transform>().position);
  }public void PlayFmodJumpEvent()
  {
    FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Jump", GetComponent<Transform>().position);
  }

}
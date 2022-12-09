// using System.Collections;
// using UnityEngine;

// public class PlayerJumpController : MonoBehaviour {

//   private float _jumpingPower = 8f;
//   private float _jumpingDeceleration = 0.5f;

//   private Transform _groundCheck;
//   private LayerMask _groundLayer;

//   internal Rigidbody2D _rb;

//   private bool _pauseGroundCheck = false;

//   private float _horizontal;

//   private bool _jump = false;
//   private bool _stopJump = false;
//   private bool _jumping = false;
//   private int _jumpCounter = 2;
//   private int _maxJumpCount = 2;

//   private bool _crouched;

//   public bool Crouched => _crouched;
//   public bool Grounded => _grounded;
//   public int JumpCount => _maxJumpCount - _jumpCounter;
//   public float SpeedX => Mathf.Abs(_rb.velocity.x);

//   internal PlayerController _player;

//   private void Awake()
//   {
//     _player = GetComponent<PlayerController>();
//     _rb = GetComponent<Rigidbody2D>();
//     _groundCheck = transform.Find("GroundCheck");
//     _groundLayer = LayerMask.GetMask("Ground");
//   }

//   private void Start()
//   {
//     AddWeapon(_defaultWeapon);
//   }

//   private void Update()
//   {
//     if (GameManager.Paused) return;

//     _headCollider.enabled = !_crouched;

//     if (!_pauseGroundCheck)
//     {
//       _grounded = IsGrounded();
//     }

//     if (_grounded)
//     {
//       _jumping = false;
//       _jumpCounter = _maxJumpCount;
//     }
    
//     if (Input.GetButtonDown("Jump") && _jumpCounter > 0 && !_crouched)
//     {
//       _jump = true;
//       _grounded = false;
//       StartCoroutine(PauseGroundCheck(0.4f));
//     }

//     if (Input.GetButtonUp("Jump") && _jumping && _rb.velocity.y > 0f)
//     {
//       _stopJump = true;
//     }
//   }

//   private void FixedUpdate()
//   {
//     if (_jump)
//     {
//       _rb.velocity = new Vector2(_rb.velocity.x, _jumpingPower); 
//       _jumpCounter--;
//       _jumping = true;
//       _jump = false;
//     }
//     if (_stopJump)
//     {
//       _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y*_jumpingDeceleration); 
//       _stopJump = false;
//     }
//   }

//   private bool IsGrounded()
//   {
//     return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
//   }

//   private IEnumerator PauseGroundCheck(float pauseTime)
//   {
//     _pauseGroundCheck = true;
//     yield return new WaitForSeconds(pauseTime);
//     _pauseGroundCheck = false;
//   }

// }
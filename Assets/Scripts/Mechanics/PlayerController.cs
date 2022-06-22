using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

  public float maxSpeed = 3f;
  public float jumpingPower = 6.5f;
  public float jumpingDeceleration = 0.5f;
  public float MoveAcceleration = 20;
  public float MoveDecceleration = 12;

  [SerializeField] private Rigidbody2D rb;
  [SerializeField] private Collider2D headCollider;
  [SerializeField] private Transform groundCheck;
  [SerializeField] private LayerMask groundLayer;

  internal Animator animator;
  internal SpriteRenderer spriteRenderer;

  public bool grounded;
  public bool crouched;
  private bool pauseGroundCheck = false;

  private float horizontal;

  private bool jump = false;
  private bool stopJump = false;
  private bool jumping = false;
  public int jumpCounter = 2;
  public int maxJumpCount = 2;

  private void Awake()
  {
    animator = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void Update()
  {
    horizontal = Input.GetAxisRaw("Horizontal");
    crouched = Input.GetAxisRaw("Vertical") == -1;

    headCollider.enabled = !crouched;

    if (!pauseGroundCheck)
    {
      grounded = IsGrounded();
    }

    if (grounded)
    {
      jumping = false;
      jumpCounter = maxJumpCount;
      // FIXME: Add deceleration instead of flat stop
      if (crouched) horizontal = 0;
    }
    
    if (Input.GetButtonDown("Jump") && jumpCounter > 0 && !crouched)
    {
      jump = true;
      grounded = false;
      StartCoroutine(PauseGroundCheck(0.4f));
    }

    if (Input.GetButtonUp("Jump") && jumping && rb.velocity.y > 0f)
    {
      stopJump = true;
    }


    animator.SetBool("Crouched", crouched);
    animator.SetInteger("JumpCount", maxJumpCount - jumpCounter);
    animator.SetBool("Grounded", IsGrounded());
    animator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
  }

  private void FixedUpdate()
  {
    if (horizontal != 0)
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

    if (jump)
    {
      rb.velocity = new Vector2(rb.velocity.x, jumpingPower); 
      jumpCounter--;
      jumping = true;
      jump = false;
    }
    if (stopJump)
    {
      rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*jumpingDeceleration); 
      stopJump = false;
    }
  }

  private bool IsGrounded()
  {
    return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
  }

  private IEnumerator PauseGroundCheck(float pauseTime)
  {
    pauseGroundCheck = true;
    yield return new WaitForSeconds(pauseTime);
    pauseGroundCheck = false;
  }

}
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

  private float maxSpeed = 3f;
  private float jumpingPower = 8f;
  private float jumpingDeceleration = 0.5f;
  private float MoveAcceleration = 20;
  private float MoveDecceleration = 12;

  [SerializeField] private Collider2D headCollider;
  [SerializeField] private GameObject defaultWeapon;
  [SerializeField] private Transform groundCheck;
  [SerializeField] private LayerMask groundLayer;
  [SerializeField] private ParticleSystem pfDamageEffect;
  
  private Transform inventory;

  internal Rigidbody2D rb;
  internal Animator animator;
  internal SpriteRenderer spriteRenderer;

  private bool grounded;
  private bool crouched;
  private bool pauseGroundCheck = false;

  private float horizontal;

  private bool jump = false;
  private bool stopJump = false;
  private bool jumping = false;
  private int jumpCounter = 2;
  private int maxJumpCount = 2;

  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    inventory = transform.Find("Inventory");

    AddWeapon(defaultWeapon);
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
      if (crouched) horizontal = 0;
    }
    
    if (Input.GetButtonDown("Jump") && jumpCounter > 0 && !crouched)
    {
      PlayFmodJumpEvent();
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

  public void AddWeapon( GameObject weapon )
  {
    inventory.GetComponent<InventoryController>().AddWeapon( weapon );
  }

  public void Damage( int var )
  {
    ParticleSystem obj = Instantiate(pfDamageEffect, transform.position, Quaternion.identity);
    Destroy( obj.gameObject, 1.0f );
  }

    public void PlayFmodFootstepsEvent()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Footsteps", GetComponent<Transform>().position);
    }public void PlayFmodJumpEvent()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Jump", GetComponent<Transform>().position);
    }

}
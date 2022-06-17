using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  private float horizontal;
  public float speed = 4f;
  public float jumpingPower = 6f;

  private bool isJumping;

  private float coyoteTime = 0.1f;
  private float coyoteTimeCounter;

  private float jumpBufferTime = 0.1f;
  private float jumpBufferCounter;

  internal Animator animator;

  [SerializeField] private Rigidbody2D rb;
  [SerializeField] private Transform groundCheck;
  [SerializeField] private LayerMask groundLayer;

  public bool grounded;

  private void Awake()
  {
    animator = GetComponent<Animator>();
  }


  private void Update()
  {
    horizontal = Input.GetAxisRaw("Horizontal");

    if (IsGrounded())
    {
      coyoteTimeCounter = coyoteTime;
      animator.SetBool("Grounded", true);
    }
    else
    {
      coyoteTimeCounter -= Time.deltaTime;
      animator.SetBool("Grounded", false);
    }

    if (Input.GetButtonDown("Jump"))
    {
      jumpBufferCounter = jumpBufferTime;
    }
    else
    {
      jumpBufferCounter -= Time.deltaTime;
    }

    if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
    {
      rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

      jumpBufferCounter = 0f;

      Debug.Log("Jump");

      StartCoroutine(JumpCooldown());
    }

    if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
    {
      rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

      coyoteTimeCounter = 0f;
    }

    animator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));

    // Flip();
  }

  private void FixedUpdate()
  {
    rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
  }

  private bool IsGrounded()
  {
    grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    return grounded;
  }

  private void Flip()
  {
    // if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
    // {
    //   Vector3 localScale = transform.localScale;
    //   isFacingRight = !isFacingRight;
    //   localScale.x *= -1f;
    //   transform.localScale = localScale;
    // }
  }

  private IEnumerator JumpCooldown()
  {
    isJumping = true;
    yield return new WaitForSeconds(0.4f);
    isJumping = false;
  }

}
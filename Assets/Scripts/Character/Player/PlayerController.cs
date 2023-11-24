using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool BlockInput = false;

    private Transform groundCheck;
    private LayerMask groundLayer;

    internal Rigidbody2D rb;
    internal Collider2D headCollider;
    internal PlayerAnimator playerAnimator;

    private float maxSpeed = 3f;
    private float jumpingPower = 8f;
    private float jumpingDeceleration = 0.5f;
    private float MoveAcceleration = 20;
    private float MoveDecceleration = 12;

    private bool pauseGroundCheck = false;
    private bool jump = false;
    private bool stopJump = false;
    private bool jumping = false;
    private int  jumpCounter = 2;
    private int  maxJumpCount = 2;

    public float InputX { get; private set; }
    public bool Crouched { get; private set; }
    public bool Grounded { get; private set; }

    public Rigidbody2D Rigidbody => rb;
    public int JumpCount => maxJumpCount - jumpCounter;

    public float VelocityX => rb.velocity.x;
    public float VelocityY => rb.velocity.y;
    public float SpeedX => Mathf.Abs(VelocityX);
    public float SpeedY => Mathf.Abs(VelocityY);
    public float DirectionX => Mathf.Sign(VelocityX);

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        headCollider = GetComponent<CircleCollider2D>();
        playerAnimator = GetComponent<PlayerAnimator>();
        groundCheck = transform.Find("GroundCheck");
        groundLayer = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        if (GameManager.Paused) return;

        InputX = BlockInput ? 0 : Input.GetAxisRaw("Horizontal");
        Crouched = BlockInput ? false : Input.GetAxisRaw("Vertical") == -1;

        headCollider.enabled = !Crouched;

        if (!pauseGroundCheck)
        {
            Grounded = IsGrounded();
        }

        if (Grounded)
        {
            jumping = false;
            jumpCounter = maxJumpCount;
            if (Crouched) InputX = 0;
        }

        if (BlockInput) return;

        if (Input.GetButtonDown("Jump") && jumpCounter > 0 && !Crouched)
        {
            playerAnimator.jump();
            jump = true;
            Grounded = false;
            StartCoroutine(PauseGroundCheck(0.4f));
        }

        if (Input.GetButtonUp("Jump") && jumping && VelocityY > 0f)
        {
            stopJump = true;
        }
    }

    private void FixedUpdate()
    {
        if (InputX != 0)
        {
            float speed = SpeedX + MoveAcceleration * Time.fixedDeltaTime;
            if (speed > maxSpeed) speed = maxSpeed;
            rb.velocity = new Vector2(InputX * speed, VelocityY);
        }
        else
        {
            float speed = SpeedX - MoveDecceleration * Time.fixedDeltaTime;
            if (speed < 0) speed = 0;
            rb.velocity = new Vector2(DirectionX * speed, VelocityY);
        }

        if (jump)
        {
            rb.velocity = new Vector2(VelocityX, jumpingPower);
            jumpCounter--;
            jumping = true;
            jump = false;
        }
        if (stopJump)
        {
            rb.velocity = new Vector2(VelocityX, VelocityY * jumpingDeceleration);
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

    public void Damage(int var)
    {
        ParticleManager.PlayDamagedPlayerEffect(transform);
    }

    public void Heal(int var)
    {
        ParticleManager.PlayHealedPlayerEffect(transform);
    }

}
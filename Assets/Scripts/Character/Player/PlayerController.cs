using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float InputX { get; private set; } = 0.0f;
    public bool Crouched { get; private set; } = false;
    public bool Grounded { get; private set; } = false;
    public bool IsTumbling { get; private set; } = false;

    public Rigidbody2D Rigidbody => rb;
    public int JumpCount => maxJumpCount - jumpCounter;

    public float VelocityX => rb.velocity.x;
    public float VelocityY => rb.velocity.y;
    public float SpeedX => Mathf.Abs(VelocityX);
    public float SpeedY => Mathf.Abs(VelocityY);
    public float DirectionX => Mathf.Sign(VelocityX);

    private PlayerInputActions playerControls;
    private InputAction moveAction;
    private InputAction crouchAction;
    private InputAction jumpAction;
    private InputAction tumbleAction;

    private Transform groundCheck;
    private LayerMask groundLayer;

    internal Rigidbody2D rb;
    internal Collider2D headCollider;
    internal PlayerAnimator playerAnimator;

    private float maxSpeed = 3f;
    private float jumpingPower = 8f;
    public float jumpingDeceleration = 0.5f;
    public float MoveAcceleration = 20;
    public float MoveDecceleration = 12;

    private bool pauseGroundCheck = false;
    private bool jump = false;
    private bool stopJump = false;
    private int jumpCounter = 2;
    private int maxJumpCount = 2;
    private float normalGravity;

    public bool canTumble = false;
    public float tumbleForce= 100;
    public float tumbleGravity = 1.5f;
    public float tumbleTime = 0.5f;
    public float tumbleDecceleration = 20;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        headCollider = GetComponent<CircleCollider2D>();
        playerAnimator = GetComponent<PlayerAnimator>();
        playerControls = new PlayerInputActions();
        groundCheck = transform.Find("GroundCheck");
        groundLayer = LayerMask.GetMask("Ground");
        normalGravity = rb.gravityScale;
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerInputActions();
        }

        moveAction = playerControls.Player.Move;
        moveAction.Enable();

        crouchAction = playerControls.Player.Crouch;
        crouchAction.performed += _ => Crouched = true;
        crouchAction.canceled += _ => Crouched = false;
        crouchAction.Enable();

        jumpAction = playerControls.Player.Jump;
        jumpAction.performed += _ => JumpStart();
        jumpAction.canceled += _ => JumpEnd();
        jumpAction.Enable();

        tumbleAction = playerControls.Player.Tumble;
        tumbleAction.performed += _ => TumbleStart();
        tumbleAction.canceled += _ => TumbleEnd();
        tumbleAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();

        crouchAction.Disable();
        crouchAction.performed -= _ => Crouched = true;
        crouchAction.canceled -= _ => Crouched = false;

        jumpAction.Disable();
        jumpAction.performed -= _ => JumpStart();
        jumpAction.canceled -= _ => JumpEnd();

        tumbleAction.Disable();
        tumbleAction.performed -= _ => TumbleStart();
        tumbleAction.canceled -= _ => TumbleEnd();
    }

    public void BlockInput()
    {
        playerControls.Disable();
    }

    public void EnableInput()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        if (GameManager.Paused) return;

        InputX = moveAction.ReadValue<Vector2>().x;

        headCollider.enabled = !Crouched;

        if (!pauseGroundCheck)
        {
            Grounded = IsGrounded();
        }

        if (Grounded && !IsTumbling)
        {
            canTumble = true;
            jumpCounter = maxJumpCount;
            if (Crouched) InputX = 0;
        }
    }

    private void FixedUpdate()
    {
        if (InputX != 0)
        {
            float speed = SpeedX + MoveAcceleration * Time.fixedDeltaTime;
            if (speed > maxSpeed)
            {
                // FIXME: This can be fixed
                if (IsTumbling)
                {
                    speed = speed - tumbleDecceleration * Time.fixedDeltaTime;
                }
                else
                {
                   speed = maxSpeed;
                }
            } 

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

    private void JumpStart()
    {
        if (jumpCounter > 0 && !Crouched)
        {
            playerAnimator.Jump();
            jump = true;
            Grounded = false;
            StartCoroutine(PauseGroundCheck(0.4f));
        }
    }

    private void JumpEnd()
    {
        stopJump = true;
    }

    private void TumbleStart()
    {
        if (canTumble)
        {
            playerAnimator.Tumble();
            // Apply force in the direction the player is facing
            Vector3 forceDirection = new Vector3(playerAnimator.IsFacingRight ? 1 : -1, 0, 0); // in the direction the player is facing
            rb.AddForce(forceDirection * tumbleForce, ForceMode2D.Impulse);
            canTumble = false; // Reenabled when grounded
            
            StartCoroutine(PerformTumble());
        }
    }

    private IEnumerator PerformTumble()
    {
        IsTumbling = true;
        rb.gravityScale = tumbleGravity;

        yield return new WaitForSeconds(tumbleTime);

        IsTumbling = false;
        rb.gravityScale = normalGravity;
    }

    private void TumbleEnd()
    {
        // TODO
    }

    public void Damage(int var)
    {
        ParticleManager.PlayDamagedPlayerEffect(transform);
        StartCoroutine(playerAnimator.FlashWhite());
    }

    public void Heal(int var)
    {
        ParticleManager.PlayHealedPlayerEffect(transform);
    }

}
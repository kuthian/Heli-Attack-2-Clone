using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    //public bool BlockInput = false;

    private PlayerInputActions playerControls;
    private InputAction moveAction;
    private InputAction crouchAction;
    private InputAction jumpAction;

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
    private int jumpCounter = 2;
    private int maxJumpCount = 2;

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
        playerControls = new PlayerInputActions();
        groundCheck = transform.Find("GroundCheck");
        groundLayer = LayerMask.GetMask("Ground");
    }

    private void OnEnable()
    {
        moveAction = playerControls.Player.Move;
        moveAction.Enable();

        crouchAction = playerControls.Player.Crouch;
        crouchAction.performed += _ => Crouched = true;
        crouchAction.canceled += _ => Crouched = false;
        crouchAction.Enable();

        jumpAction = playerControls.Player.Jump;
        jumpAction.performed += JumpStart;
        jumpAction.canceled += JumpEnd;
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        crouchAction.Disable();
        jumpAction.Disable();
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

        if (Grounded)
        {
            jumpCounter = maxJumpCount;
            if (Crouched) InputX = 0;
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

    private void JumpStart(InputAction.CallbackContext context)
    {
        if (jumpCounter > 0 && !Crouched)
        {
            playerAnimator.jump();
            jump = true;
            Grounded = false;
            StartCoroutine(PauseGroundCheck(0.4f));
        }
    }

    private void JumpEnd(InputAction.CallbackContext context)
    {
        rb.velocity = new Vector2(VelocityX, VelocityY * jumpingDeceleration);
        stopJump = true;
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
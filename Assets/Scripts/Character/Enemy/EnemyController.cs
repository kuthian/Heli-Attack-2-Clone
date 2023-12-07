using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    internal Rigidbody2D rb;
    private EnemyGunController gunController;
    private Transform player;

    public bool IdleOnly = false;
    public bool LeaveForever = false;
    public bool ShootingEnabled => GetComponentInChildren<EnemyGunController>().ShootingEnabled;
    public bool IsFlipped = false;

    public enum State
    {
        WakingUp,
        Idle,
        Leaving,
        Returning
    }

    [Serializable]
    struct StateParameters
    {
        public Vector2 maxSpeed;
        public Vector2 acceleration;
        public Vector2 decceleration;
        public float tolerance;
        public float durationSeconds;
    };

    [Header("Current State")]
    [SerializeField]
    private State state = State.WakingUp;
    private DateTime nextStateTime = DateTime.Now;

    [Header("State Parameters")]
    [SerializeField]
    private StateParameters idle;
    [SerializeField]
    private StateParameters leaving;
    [SerializeField]
    private StateParameters returning;

    [Header("X Tracking Parameters")]
    public float A0 = 0;
    public float Freq = 1;
    public float Amplitude = 1;
    public float Phase = 0;
    private float trackingOffsetX = 0;
    private float time = 0;

    [Header("Y Tracking Parameters")]
    [SerializeField]
    private float[] _possibleCruisingPositionsY;
    [SerializeField]
    private float cruisingPositionY = 1.0f;
    [SerializeField]
    private float cruisingToleranceY = 0.2f;

    private float PlayerPosX => player.position.x + trackingOffsetX;
    private float PlayerPosY => player.position.y;

    private int DirectionToPlayerX => (int)Mathf.Sign(PlayerPosX - transform.position.x);
    private int DirectionToPlayerY => (int)Mathf.Sign(PlayerPosY - transform.position.y);
    private float DistanceFromPlayerX => transform.position.x - PlayerPosX;
    private float DistanceFromPlayerY => transform.position.y - PlayerPosY;
    private bool MovingTowardsPlayerX => DirectionToPlayerX == DirectionX;
    private bool MovingTowardsPlayerY => DirectionToPlayerY == DirectionY;

    private float DistanceFromCruisingY => MathF.Abs(cruisingPositionY - transform.position.y);
    private int DirectionToCruisingY => (int)Mathf.Sign(cruisingPositionY - transform.position.y);
    private bool MovingTowardsCruising => DirectionToCruisingY == DirectionY;

    private float VelocityX => rb.velocity.x;
    private float VelocityY => rb.velocity.y;
    private float SpeedX => Mathf.Abs(VelocityX);
    private float SpeedY => Mathf.Abs(VelocityY);
    private int DirectionX => VelocityX == 0 ? 0 : (int)Mathf.Sign(VelocityX);
    private int DirectionY => VelocityY == 0 ? 0 : (int)Mathf.Sign(VelocityY);

    private bool NotMovingX => SpeedX == 0;
    private bool NotMovingY => SpeedY == 0;
    private bool MovingX => SpeedX > 0;
    private bool MovingY => SpeedY > 0;

    void Awake()
    {
        state = State.WakingUp;
        nextStateTime = DateTime.Now;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
        gunController = GetComponentInChildren<EnemyGunController>();
        GetComponentInChildren<AimAtTransform>().Target = player;
        GetComponentInChildren<EnemyGunController>().ObjectToIgnore = gameObject;
    }

    void FixedUpdate()
    {
        if (DateTime.Now > nextStateTime)
        {
            GoToNextState();
        }
        CalculateMovement();
        CalculateRotation();
    }

    private void GoToNextState()
    {
        switch (state)
        {
            case State.WakingUp:
                GoToState(State.Returning);
                break;
            case State.Idle:
                GoToState(IdleOnly ? State.Idle : State.Leaving);
                break;
            case State.Leaving:
                // Update cruising position so that the helicopter
                // always returns to a different cruising distance
                cruisingPositionY = Utils.RandomInRange(_possibleCruisingPositionsY);
                GoToState(LeaveForever ? State.Leaving : State.Returning);
                break;
            case State.Returning:
                GoToState(State.Idle);
                break;
        }
    }

    public void GoToState(State nextState)
    {
        // var previous_state = _state;
        gunController.ShootingEnabled = (nextState == State.Idle);
        nextStateTime = DateTime.Now.AddSeconds(GetStateParameters(nextState).durationSeconds);
        state = nextState;
        // Debug.Log(previous_state + " -> " + _state );
    }

    private StateParameters GetStateParameters(State state)
    {
        switch (state)
        {
            case State.Idle: return idle;
            case State.Leaving: return leaving;
            case State.Returning: return returning;
            default: return idle;
        }
    }

    // Note: These movement functions might be able to be replaced by :
    // Vector2.SmoothDamp, Vector2.MoveTowards, and Vector2.ClampMagnitude 

    private void MoveTowardsPlayerX()
    {
        if (MovingTowardsPlayerX || NotMovingX)
        {
            //Debug.Log("MoveTowardsPlayerX");
            var m = GetStateParameters(state);
            float speed = SpeedX + m.acceleration.x * Time.fixedDeltaTime;
            if (speed > m.maxSpeed.x) speed = m.maxSpeed.x;
            rb.velocity = new Vector2(DirectionToPlayerX * speed, VelocityY);
        }
        else
        {
            SlowDownX();
        }
    }

    private void MoveTowardsPlayerY()
    {
        if (MovingTowardsPlayerY || NotMovingY)
        {
            //Debug.Log("MoveTowardsPlayerY");
            var m = GetStateParameters(state);
            float speed = SpeedY + m.acceleration.y * Time.fixedDeltaTime;
            if (speed > m.maxSpeed.y) speed = m.maxSpeed.y;
            rb.velocity = new Vector2(VelocityX, DirectionToPlayerY * speed);
        }
        else
        {
            SlowDownY();
        }
    }

    private void MoveAwayFromPlayerX()
    {
        if (MovingTowardsPlayerX)
        {
            SlowDownX();
            return;
        }
        //Debug.Log("MoveAwayFromPlayerX");
        var m = GetStateParameters(state);
        float speed = SpeedX + m.acceleration.x * Time.fixedDeltaTime;
        if (speed > m.maxSpeed.x) speed = m.maxSpeed.x;
        rb.velocity = new Vector2(-1 * DirectionToPlayerX * speed, VelocityY);
    }

    private void MoveAwayFromPlayerY()
    {
        if (MovingTowardsPlayerY)
        {
            SlowDownY();
            return;
        }
        //Debug.Log("MoveAwayFromPlayerY");
        var m = GetStateParameters(state);
        float speed = SpeedY + m.acceleration.y * Time.fixedDeltaTime;
        if (speed > m.maxSpeed.y) speed = m.maxSpeed.y;
        rb.velocity = new Vector2(VelocityX, -1 * DirectionToPlayerY * speed);
    }

    private void SlowDownX()
    {
        // Debug.Log("SlowDownX");
        var m = GetStateParameters(state);
        float speed = SpeedX - m.decceleration.x * Time.fixedDeltaTime;
        if (speed < 0) speed = 0;
        rb.velocity = new Vector2(DirectionX * speed, VelocityY);
    }

    private void SlowDownY()
    {
        // Debug.Log("SlowDownY");
        var m = GetStateParameters(state);
        float speed = SpeedY - m.decceleration.y * Time.fixedDeltaTime;
        if (speed < 0) speed = 0;
        rb.velocity = new Vector2(VelocityX, DirectionY * speed);
    }

    private void MoveToCruising()
    {
        if (MovingTowardsPlayerY || NotMovingY)
        {
            // Debug.Log("MoveToCruisingY");
            var m = GetStateParameters(state);
            float speed = SpeedY + m.acceleration.y * Time.fixedDeltaTime;
            if (speed > m.maxSpeed.y) speed = m.maxSpeed.y;
            rb.velocity = new Vector2(VelocityX, DirectionToCruisingY * speed);
        }
        else
        {
            SlowDownY();
        }
    }

    void IdleMovement()
    {
        var m = GetStateParameters(State.Idle);

        time += Time.fixedDeltaTime;
        trackingOffsetX = A0 + Amplitude * (float)Math.Sin(Freq * 2 * Math.PI * time + Phase);

        bool withinToleranceX = Mathf.Abs(DistanceFromPlayerX) < m.tolerance;
        if (!withinToleranceX)
        {
            MoveTowardsPlayerX();
        }
        else
        if (MovingX)
        {
            SlowDownX();
        }

        bool withinToleranceY = Mathf.Abs(DistanceFromCruisingY) < cruisingToleranceY;
        if (!withinToleranceY)
        {
            MoveToCruising();
        }
        else
        if (MovingY)
        {
            SlowDownY();
        }
    }

    void LeavingMovement()
    {
        MoveAwayFromPlayerY();
        MoveAwayFromPlayerX();
    }

    void ReturningMovement()
    {
        var m = returning;

        bool withinToleranceX = Mathf.Abs(DistanceFromPlayerX) < m.tolerance;
        if (!withinToleranceX)
        {
            MoveTowardsPlayerX();
        }
        else
        {
            SlowDownX();
            gunController.ShootingEnabled = true;
        }

        bool withinToleranceY = Mathf.Abs(DistanceFromCruisingY) < m.tolerance;
        if (!withinToleranceY)
        {
            MoveToCruising();
        }
        else
        if (MovingY)
        {
            SlowDownY();
        }

        if (withinToleranceY && withinToleranceX &&
            SpeedX < 1.3 && SpeedY < 1.3)
        {
            GoToState(State.Idle);
        }
    }

    void CalculateMovement()
    {
        switch (state)
        {
            case State.Idle:
                IdleMovement();
                return;
            case State.Leaving:
                LeavingMovement();
                return;
            case State.Returning:
                ReturningMovement();
                return;
        }
    }

    void CalculateRotation()
    {
        int direction = IsFlipped ? 1 : -1;
        int rotationY = IsFlipped ? 180 : 0; // Flip the sprite
        Quaternion r = transform.rotation;
        r = new Quaternion(r.x, rotationY, r.z, r.w);
        r.eulerAngles = new Vector3(r.eulerAngles.x, r.eulerAngles.y, direction * 5.0f * VelocityX);
        transform.rotation = new Quaternion(r.x, r.y, r.z, r.w);
    }

}
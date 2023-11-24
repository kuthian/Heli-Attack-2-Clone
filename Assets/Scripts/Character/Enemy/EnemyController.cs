using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    internal Rigidbody2D _rb;
    private EnemyGunController _gunController;
    private Transform _player;

    public bool IdleOnly = false;
    public bool LeaveForever = false;

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

    [SerializeField]
    private State _state = State.WakingUp;
    private DateTime _nextStateTime = DateTime.Now;

    [SerializeField]
    private StateParameters _idle;
    [SerializeField]
    private StateParameters _leaving;
    [SerializeField]
    private StateParameters _returning;

    [SerializeField]
    private float[] _possibleTrackingOffsetsX;
    [SerializeField]
    private float _trackingOffsetX = 0;

    private float PlayerPosX => _player.position.x + _trackingOffsetX;
    private float PlayerPosY => _player.position.y;

    private int DirectionToPlayerX => (int)Mathf.Sign(PlayerPosX - transform.position.x);
    private int DirectionToPlayerY => (int)Mathf.Sign(PlayerPosY - transform.position.y);
    private float DistanceFromPlayerX => transform.position.x - PlayerPosX;
    private float DistanceFromPlayerY => transform.position.y - PlayerPosY;
    private bool MovingTowardsPlayerX => DirectionToPlayerX == DirectionX;
    private bool MovingTowardsPlayerY => DirectionToPlayerY == DirectionY;

    [SerializeField]
    private float[] _possibleCruisingPositionsY;
    [SerializeField]
    private float _cruisingPositionY = 1.0f;
    [SerializeField]
    private float _cruisingToleranceY = 0.2f;

    private float DistanceFromCruisingY => MathF.Abs(_cruisingPositionY - transform.position.y);
    private int DirectionToCruisingY => (int)Mathf.Sign(_cruisingPositionY - transform.position.y);
    private bool MovingTowardsCruising => DirectionToCruisingY == DirectionY;

    private float VelocityX => _rb.velocity.x;
    private float VelocityY => _rb.velocity.y;
    private float SpeedX => Mathf.Abs(VelocityX);
    private float SpeedY => Mathf.Abs(VelocityY);
    private int DirectionX => VelocityX == 0 ? 0 : (int)Mathf.Sign(VelocityX);
    private int DirectionY => VelocityY == 0 ? 0 : (int)Mathf.Sign(VelocityY);

    private bool NotMovingX => SpeedX == 0;
    private bool NotMovingY => SpeedY == 0;
    private bool MovingX => SpeedX > 0;
    private bool MovingY => SpeedY > 0;

    bool ShootingEnabled => GetComponentInChildren<EnemyGunController>().ShootingEnabled;

    void Awake()
    {
        _state = State.WakingUp;
        _nextStateTime = DateTime.Now;
        _trackingOffsetX = Utils.RandomInRange(_possibleTrackingOffsetsX);
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Player").transform;
        _gunController = GetComponentInChildren<EnemyGunController>();
        GetComponentInChildren<AimAtTransform>().Target = _player;
        GetComponentInChildren<EnemyGunController>().ObjectToIgnore = gameObject;
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        if (DateTime.Now > _nextStateTime)
        {
            GoToNextState();
        }
        CalculateMovement();
        CalculateRotation();
    }

    private void GoToNextState()
    {
        switch (_state)
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
                _cruisingPositionY = Utils.RandomInRange(_possibleCruisingPositionsY);
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
        _gunController.ShootingEnabled = (nextState == State.Idle);
        _nextStateTime = DateTime.Now.AddSeconds(GetStateParameters(nextState).durationSeconds);
        _state = nextState;
        // Debug.Log(previous_state + " -> " + _state );
    }

    private StateParameters GetStateParameters(State state)
    {
        switch (state)
        {
            case State.Idle: return _idle;
            case State.Leaving: return _leaving;
            case State.Returning: return _returning;
            default: return _idle;
        }
    }

    // Note: These movement functions might be able to be replaced by :
    // Vector2.SmoothDamp, Vector2.MoveTowards, and Vector2.ClampMagnitude 

    private void MoveTowardsPlayerX()
    {
        if (MovingTowardsPlayerX || NotMovingX)
        {
            //Debug.Log("MoveTowardsPlayerX");
            var m = GetStateParameters(_state);
            float speed = SpeedX + m.acceleration.x * Time.fixedDeltaTime;
            if (speed > m.maxSpeed.x) speed = m.maxSpeed.x;
            _rb.velocity = new Vector2(DirectionToPlayerX * speed, VelocityY);
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
            var m = GetStateParameters(_state);
            float speed = SpeedY + m.acceleration.y * Time.fixedDeltaTime;
            if (speed > m.maxSpeed.y) speed = m.maxSpeed.y;
            _rb.velocity = new Vector2(VelocityX, DirectionToPlayerY * speed);
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
        var m = GetStateParameters(_state);
        float speed = SpeedX + m.acceleration.x * Time.fixedDeltaTime;
        if (speed > m.maxSpeed.x) speed = m.maxSpeed.x;
        _rb.velocity = new Vector2(-1 * DirectionToPlayerX * speed, VelocityY);
    }

    private void MoveAwayFromPlayerY()
    {
        if (MovingTowardsPlayerY)
        {
            SlowDownY();
            return;
        }
        //Debug.Log("MoveAwayFromPlayerY");
        var m = GetStateParameters(_state);
        float speed = SpeedY + m.acceleration.y * Time.fixedDeltaTime;
        if (speed > m.maxSpeed.y) speed = m.maxSpeed.y;
        _rb.velocity = new Vector2(VelocityX, -1 * DirectionToPlayerY * speed);
    }

    private void SlowDownX()
    {
        // Debug.Log("SlowDownX");
        var m = GetStateParameters(_state);
        float speed = SpeedX - m.decceleration.x * Time.fixedDeltaTime;
        if (speed < 0) speed = 0;
        _rb.velocity = new Vector2(DirectionX * speed, VelocityY);
    }

    private void SlowDownY()
    {
        // Debug.Log("SlowDownY");
        var m = GetStateParameters(_state);
        float speed = SpeedY - m.decceleration.y * Time.fixedDeltaTime;
        if (speed < 0) speed = 0;
        _rb.velocity = new Vector2(VelocityX, DirectionY * speed);
    }

    private void MoveToCruising()
    {
        if (MovingTowardsPlayerY || NotMovingY)
        {
            // Debug.Log("MoveToCruisingY");
            var m = GetStateParameters(_state);
            float speed = SpeedY + m.acceleration.y * Time.fixedDeltaTime;
            if (speed > m.maxSpeed.y) speed = m.maxSpeed.y;
            _rb.velocity = new Vector2(VelocityX, DirectionToCruisingY * speed);
        }
        else
        {
            SlowDownY();
        }
    }

    void IdleMovement()
    {
        var m = GetStateParameters(State.Idle);

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

        bool withinToleranceY = Mathf.Abs(DistanceFromCruisingY) < _cruisingToleranceY;
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
        var m = _returning;

        bool withinToleranceX = Mathf.Abs(DistanceFromPlayerX) < m.tolerance;
        if (!withinToleranceX)
        {
            MoveTowardsPlayerX();
        }
        else
        {
            SlowDownX();
            _gunController.ShootingEnabled = true;
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
        switch (_state)
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
        Quaternion r = transform.rotation;
        r.eulerAngles = new Vector3(r.eulerAngles.x, r.eulerAngles.y, -5.0f * VelocityX);
        transform.rotation = new Quaternion(r.x, r.y, r.z, r.w);
    }

}
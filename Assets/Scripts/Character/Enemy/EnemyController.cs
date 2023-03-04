// using System.Collections;
using System;
using UnityEngine;

public class EnemyController : MonoBehaviour {

  enum State
  {
    WakingUp,
    Idle,
    Leaving,
    // Left,
    Returning
  }

  // Vector2

  [Serializable]
  struct StateParameters {
    public float maxSpeed;
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

  private Transform _player;

  private int DirectionToPlayerX => (int)Mathf.Sign(_player.position.x - transform.position.x);
  private int DirectionToPlayerY => (int)Mathf.Sign(_player.position.y - transform.position.y);
  private float DistanceFromPlayerX => transform.position.x - _player.position.x;
  private float DistanceFromPlayerY => transform.position.y - _player.position.y;
  private float DistanceFromGroundY => transform.position.y + 3.5f;

  private bool MovingTowardsPlayerX => DirectionToPlayerX == DirectionX;
  private bool MovingTowardsPlayerY => DirectionToPlayerY == DirectionY;

  private EnemyGunController _gunController;

  internal Rigidbody2D _rb;

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

  void Awake()
  {
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
    if ( DateTime.Now > _nextStateTime )
    {
      GoToNextState();
    }
    CalculateMovement();
    CalculateRotation();
  }

  private void GoToNextState()
  {
    switch (_state) {
      case State.WakingUp:
        GoToState( State.Idle );
        break;
      case State.Idle:
        GoToState( State.Idle );
        break;
      case State.Leaving:
        GoToState( State.Returning );
        break;
      case State.Returning:
        GoToState( State.Idle );
        break;
    }
  }

  void GoToState( State nextState )
  {
    _gunController.ShootingEnabled = ( nextState == State.Idle );
    _nextStateTime = DateTime.Now.AddSeconds( GetStateParameters(nextState).durationSeconds );
    _state = nextState;
  }

  private StateParameters GetStateParameters( State state )
  {
    switch (state) {
      case State.Idle: return _idle;
      case State.Leaving: return _leaving;
      case State.Returning: return _returning;
      default: return _idle;
    }    
  }

  private void MoveTowardsPlayerX()
  {
    if ( MovingTowardsPlayerX || NotMovingX )
    {
      Debug.Log("MoveTowardsPlayerX");
      var m = GetStateParameters(_state);
      float speed = SpeedX + m.acceleration.x * Time.fixedDeltaTime;
      if (speed > m.maxSpeed) speed = m.maxSpeed;
      _rb.velocity = new Vector2(DirectionToPlayerX * speed, VelocityY);
    } 
    else
    {
      SlowDownX();
    }
  }

  private void MoveTowardsPlayerY()
  {
    if ( MovingTowardsPlayerY || NotMovingY )
    {
      Debug.Log("MoveTowardsPlayerY");
      var m = GetStateParameters(_state);
      float speed = SpeedY + m.acceleration.y * Time.fixedDeltaTime;
      if (speed > m.maxSpeed) speed = m.maxSpeed;
      _rb.velocity = new Vector2(VelocityX, DirectionToPlayerY * speed);
    }
    else
    {
      SlowDownY();
    }
  }

  private void MoveAwayFromPlayerX()
  {
    if ( MovingTowardsPlayerX ) {
      SlowDownX();
      return;
    }
    Debug.Log("MoveAwayFromPlayerX");
    var m = GetStateParameters(_state);
    float speed = SpeedX + m.acceleration.x * Time.fixedDeltaTime;
    if (speed > m.maxSpeed) speed = m.maxSpeed;
    _rb.velocity = new Vector2(-1*DirectionToPlayerX * speed, VelocityY);
  }

  private void MoveAwayFromPlayerY()
  {
    if ( MovingTowardsPlayerY ) {
      SlowDownY();
      return;
    }
    Debug.Log("MoveAwayFromPlayerY");
    var m = GetStateParameters(_state);
    float speed = SpeedY + m.acceleration.y * Time.fixedDeltaTime;
    if (speed > m.maxSpeed) speed = m.maxSpeed;
    _rb.velocity = new Vector2(VelocityX, -1*DirectionToPlayerY * speed);
  }

  private void SlowDownX()
  {
    Debug.Log("SlowDownX");
    var m = GetStateParameters(_state);
    float speed = SpeedX - m.decceleration.x * Time.fixedDeltaTime;
    if (speed < 0) speed = 0;
    _rb.velocity = new Vector2(DirectionX * speed, VelocityY);
  }

  private void SlowDownY()
  {
    Debug.Log("SlowDownY");
    var m = GetStateParameters(_state);
    float speed = SpeedY - m.decceleration.y * Time.fixedDeltaTime;
    if (speed < 0) speed = 0;
    _rb.velocity = new Vector2(VelocityX, DirectionY * speed);
  }

  private void MoveToCruisingHeight()
  {
    if ( MovingTowardsPlayerY || NotMovingY )
    {
      Debug.Log("MoveTowardsPlayerY");
      var m = GetStateParameters(_state);
      float speed = SpeedY + m.acceleration.y * Time.fixedDeltaTime;
      if (speed > m.maxSpeed) speed = m.maxSpeed;
      _rb.velocity = new Vector2(VelocityX, DirectionToPlayerY * speed);
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

    if ( !withinToleranceX )
    // && (MovingTowardsPlayerX || NotMovingX)  )
    {
      MoveTowardsPlayerX();
    }
    else
    if ( MovingX )
    {
      SlowDownX();
    }

    // Debug.Log("Distance from player: " + DistanceFromPlayerY + 
    //           ", Distance from ground: " + (transform.position.y + 3.5f));

    // float minDistanceFromTarget = 3;
    // float maxDistanceFromTarget = 5;
    // float cruisingPositionY = 1.0f;
    // float cruisingToleranceY = 0.2f;

    // float DistanceToCruisingY = MathF.Abs(cruisingPositionY - transform.position.y);
    // int DirectionToCruisingY = (int)Mathf.Sign(cruisingPositionY - transform.position.y);
    // bool MovingTowardsCruising = DirectionToCruisingY == DirectionY;


    // Debug.Log("Distance: " + DistanceToCruisingY + ", MovingTowards: " + MovingTowardsCruising );
    // Debug.Log("MovingTowards: " + MovingTowardsCruising);
    // Debug.Log("NotMovingY: " + NotMovingY);
    // Debug.Log("DirectionToCruisingY: " + DirectionToCruisingY);

    // bool withinCruisingTolerance = Mathf.Abs(DistanceToCruisingY) < cruisingToleranceY;

    // if (!withinCruisingTolerance)
    // {
    //   if ( MovingTowardsCruising || NotMovingY )
    //   {
    //     Debug.Log("MoveTowardsCruising");
    //     // var m = GetStateParameters(_state);
    //     float speed = SpeedY + m.acceleration.y * Time.fixedDeltaTime;
    //     if (speed > m.maxSpeed) speed = m.maxSpeed;
    //     _rb.velocity = new Vector2(VelocityX, DirectionToCruisingY * speed);
    //   }
    //   else
    //   {
    //     Debug.Log("MoveTowardsCruising");
    //     SlowDownY();
    //   }
    // }
    // else
    // if ( MovingY )
    // {
    //   SlowDownY();
    // }
    // Debug.Log("DirectionToCruisingPositionY: " + DirectionToCruisingPositionY);
    // Debug.Log("maxDistanceFromTarget: " + maxDistanceFromTarget);
    // Debug.Log("VelocityY: " + VelocityY);

    _rb.velocity = new Vector2(VelocityX, 0.0f);


    // float tolerance = 0.25f;
    // bool tooFar = DistanceFromGroundY > maxDistanceFromTarget;
    // bool tooClose = (DistanceFromPlayerY + tolerance) < minDistanceFromTarget;
    // if ( tooClose )
    // // && (!MovingTowardsPlayerY || NotMovingY ) )
    // {
    //   // Debug.Log("Moving Away");
    //   // Move away from target
    //   // float speed = SpeedY + 2.0f * Time.fixedDeltaTime;
    //   MoveAwayFromPlayerY();
    //   // _rb.velocity = new Vector2(VelocityX, 1.0f*speed);
    // }
    // else 
    // if ( tooFar )
    // // && (MovingTowardsPlayerY || NotMovingY ) )
    // {
    //   // Debug.Log("Moving Towards");
    //   // Move towards target
    //   // int direction = _rb.velocity.y > 0 ? 1 : -1;
    //   // int direction = -1;
    //   // float speed = SpeedY + 2.0f * Time.fixedDeltaTime;
    //   // if (speed > m.maxSpeed) speed = m.maxSpeed;
    //   // _rb.velocity = new Vector2(VelocityX, direction * speed);
    //   MoveTowardsPlayerY();
    // }
    // else
    // if ( MovingY )
    // {
    //   // SlowDown
    //   SlowDownY();
    // }
  }

  void LeavingMovement()
  {
    var m = _leaving;

    MoveAwayFromPlayerY();
    MoveAwayFromPlayerX();

    // float speed = SpeedX + m.acceleration * Time.fixedDeltaTime;
    // if (speed > m.maxSpeed) speed = m.maxSpeed;
    // // int direction = MathF.Sign(_rb.velocity.x);
    // // if (direction == 0) direction = Math.Sign(UnityEngine.Random.Range(-100,100));
    // _rb.velocity = new Vector2(DirectionX * speed, VelocityY);

    // float speed = _rb.velocity.x + Math.Sign(_rb.velocity.x) * m.acceleration * Time.fixedDeltaTime;
    // if (speed > m.maxSpeed) speed = m.maxSpeed;
    // _rb.velocity = new Vector2(speed, _rb.velocity.y);


    // MathF.Sign(_rb.velocity.x) * 
    // if ( velocityX > 0 )
    // {
    //   velocityX += m.acceleration * Time.fixedDeltaTime;
    //   if (velocityX > m.maxSpeed) velocityX = m.maxSpeed;
    // }
    // else
    // {
    //   velocityX -= m.acceleration * Time.fixedDeltaTime;
    // }
    // float speed = Mathf.Abs(_rb.velocity.x) + _MoveAcceleration * Time.fixedDeltaTime;
    // // if (speed > _maxSpeed) speed = _maxSpeed;
    // float xDistance = transform.position.x - _player.position.x;
    // int direction = xDistance > 0 ? 1 : -1;
    // Debug.Log("horizontal=" + _horizontal + ", direction=" + direction);
    // _rb.velocity = new Vector2(velocityX, _rb.velocity.y);
  }

  void ReturningMovement()
  {
    var m = _returning;

    bool withinToleranceX = Mathf.Abs(DistanceFromPlayerX) < m.tolerance;
    if ( !withinToleranceX )
    {
      MoveTowardsPlayerX();
    }
    else
    {
      SlowDownX();
      if (SpeedX < 3) {
        GoToState( State.Idle );
      }
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
using UnityEngine;
using UnityEngine.InputSystem;

public class AimWithJoystick : AimAtTarget
{
    public Vector3 Target;
    public float radius = 2.0f; // Radius of the circle
    public Transform circleCenter;
    public InputAction aimAction;

    private float angle = 0.0f;

    private void OnEnable()
    {
        aimAction.Enable();
    }

    private void OnDisable()
    {
        aimAction.Disable();
    }

    public void Awake()
    {
        Target = new Vector3();
    }

    private void Update()
    {
        Vector2 joystickInput = aimAction.ReadValue<Vector2>();

        // Check if the joystick is being actively moved
        if (joystickInput.magnitude > 0.1f)
        {
            angle = Mathf.Atan2(joystickInput.y, joystickInput.x);
        }

        // Convert the angle to a point on the circle
        Target = circleCenter.position + new Vector3(
            Mathf.Cos(angle) * radius,
            Mathf.Sin(angle) * radius,
            0 // Assuming a circle on the XY plane
        );

        Utils.DrawSquare(Target, 2);

        HandleAiming();
    }

    protected override Vector3 GetTargetPosition()
    {
        Debug.Log(Target);
        return Target;
    }
}
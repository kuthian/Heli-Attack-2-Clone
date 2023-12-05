using UnityEngine;
using UnityEngine.InputSystem;

public class AimAtMouse : AimAtTarget
{
    protected override Vector3 GetTargetPosition()
    {
        return Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }
}

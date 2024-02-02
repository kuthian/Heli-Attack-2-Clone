using Unity.VisualScripting;
using UnityEngine;

public abstract class AimAtTarget : MonoBehaviour
{
    virtual protected void Update()
    {
        if (GameManager.Paused) return;
        HandleAiming();
    }

    protected abstract Vector3 GetTargetPosition();

    protected void HandleAiming()
    {
        Vector3 targetPosition = GetTargetPosition();
        targetPosition.z = 0.0f;
        Vector3 aimDirection = (targetPosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        angle = ModifyAimAngle(angle);
        transform.eulerAngles = new Vector3(0, 0, angle);

        bool FacingRight = angle < 90.0f && angle > -90.0f;
        if (FacingRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
    }

    virtual protected float ModifyAimAngle(float angle)
    {
        return angle;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimAtTarget : MonoBehaviour {

  private void Update()
  {
    HandleAiming();
  }

  protected abstract Vector3 GetTargetPosition();

  protected void HandleAiming()
  {
    if ( GameManager.Paused ) return;

    Vector3 targetPosition = GetTargetPosition();
    targetPosition.z = 0.0f;
    Vector3 aimDirection = (targetPosition - transform.position).normalized;
    float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
    transform.eulerAngles = new Vector3(0, 0, angle);

    bool FacingRight = angle < 90.0f && angle > -90.0f;
    if (FacingRight) {
      transform.localScale = new Vector3(1, 1, 1);
    } else {
      transform.localScale = new Vector3(1, -1, 1);
    }
  }
}

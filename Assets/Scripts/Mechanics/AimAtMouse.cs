using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtMouse : MonoBehaviour
{
  [SerializeField] private SpriteRenderer spriteRenderer;

  public event EventHandler<OnClickEventArgs> OnClick;
  public class OnClickEventArgs : EventArgs {
    public Vector3 clickPosition;
  };

  public event EventHandler<EventArgs> OnClickReleased;

  private void Update()
  {
    HandleAiming();
    HandleClicking();
  }

  private void HandleAiming()
  {
    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    mousePosition.z = 0.0f;
    Vector3 aimDirection = (mousePosition - transform.position).normalized;
    float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
    transform.eulerAngles = new Vector3(0, 0, angle);

    bool FacingRight = angle < 90.0f && angle > -90.0f;
    if (FacingRight) {
      transform.localScale = new Vector3(1, 1, 1);
    } else {
      transform.localScale = new Vector3(1, -1, 1);
    }
  }

  private void HandleClicking()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      mousePosition.z = 0.0f;
      OnClick?.Invoke(this, 
          new OnClickEventArgs {
            clickPosition = mousePosition,
      });
    }
    if (Input.GetMouseButtonUp(0))
    {
      OnClickReleased?.Invoke(this, new EventArgs());
    }
  }
}

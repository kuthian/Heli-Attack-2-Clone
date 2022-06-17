using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtMouse : MonoBehaviour
{
  [SerializeField] private SpriteRenderer spriteRenderer;

  private void Update()
  {
    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector3 aimDirection = (mousePosition - transform.position).normalized;
    float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
    transform.eulerAngles = new Vector3(0, 0, angle);

    if (spriteRenderer) {
      bool FacingRight = angle < 90.0f && angle > -90.0f;
      spriteRenderer.flipY = !FacingRight;
    }

  }
}

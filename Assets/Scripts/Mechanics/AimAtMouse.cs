using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtMouse : AimAtTarget {

  protected override Vector3 GetTargetPosition()
  {
    return Camera.main.ScreenToWorldPoint(Input.mousePosition);
  }

}

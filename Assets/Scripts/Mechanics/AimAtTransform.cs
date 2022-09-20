using UnityEngine;

public class AimAtTransform : AimAtTarget {
  
  public Transform target;

  protected override Vector3 GetTargetPosition()
  {
    return target.position;
  }
}
using UnityEngine;

public class AimAtTransform : AimAtTarget {
  
  public Transform Target { get; set; }

  protected override Vector3 GetTargetPosition()
  {
    return Target.position;
  }
}
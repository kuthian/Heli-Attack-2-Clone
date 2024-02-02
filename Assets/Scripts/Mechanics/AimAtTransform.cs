using UnityEngine;

public class AimAtTransform : AimAtTarget
{
    [field: SerializeField]
    public Transform Target { get; set; }

    protected override Vector3 GetTargetPosition()
    {
        return Target.position;
    }
}
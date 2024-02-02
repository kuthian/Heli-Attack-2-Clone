using UnityEngine;

public class AimAtTransformWithOscillation : AimAtTransform
{
    public float amplitude = 0.5f;
    public float frequency = 1.0f;

    private float elapsedTime = 0.0f;

    protected override void Update()
    {
        elapsedTime += Time.deltaTime;
        base.Update();
    }

    protected override float ModifyAimAngle(float angle)
    {
        return angle + amplitude * Mathf.Sin(frequency * 2 * Mathf.PI * elapsedTime);
    }
}

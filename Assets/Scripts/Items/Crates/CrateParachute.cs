using UnityEngine;

public class CrateParachute : MonoBehaviour
{
    public int ParachuteDrag = 10;
    internal SpriteRenderer spriteRenderer;
    internal CrateController crate;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        crate = GetComponentInParent<CrateController>();
        DeployParachute();
    }

    private void DeployParachute()
    {
        if (crate)
        {
            crate.Rigidbody.drag = ParachuteDrag;
        }
        if (spriteRenderer)
        {
            spriteRenderer.enabled = true;
        }
    }

    private void OnEnable()
    {
        DeployParachute();
    }

    private void OnDisable()
    {
        crate.Rigidbody.drag = 0;
        spriteRenderer.enabled = false;
    }

    private void FixedUpdate()
    {
        if (crate.Grounded)
        {
            enabled = false;
        }
    }
}

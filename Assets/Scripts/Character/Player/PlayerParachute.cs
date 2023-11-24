using UnityEngine;

public class PlayerParachute : MonoBehaviour
{
    public int ParachuteDrag = 10;
    internal SpriteRenderer spriteRenderer;
    internal PlayerController player;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponentInParent<PlayerController>();
        DeployParachute();
    }

    private void DeployParachute()
    {
        if (player)
        {
            player.Rigidbody.drag = ParachuteDrag;
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
        player.Rigidbody.drag = 0;
        spriteRenderer.enabled = false;
    }

    void FixedUpdate()
    {
        if (player.Grounded)
        {
            enabled = false;
        }
    }

};
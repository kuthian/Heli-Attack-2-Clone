using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    internal SpriteRenderer spriteRenderer;
    internal Animator animator;
    internal PlayerController player;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerController>();
        animator.SetBool("isDead", false);
    }

    private void Update()
    {
        if (GameManager.Paused) return;

        animator.SetBool("Crouched", player.Crouched);
        animator.SetInteger("JumpCount", player.JumpCount);
        animator.SetBool("Grounded", player.Grounded);
        animator.SetFloat("VelocityX", player.SpeedX);
        if (player.InputX != 0)
        {
            spriteRenderer.flipX = player.InputX == -1;
        }
    }

    public void jump()
    {
        animator.SetTrigger("Jump");
    }

    public void StartDeathSequence()
    {
        animator.SetTrigger("Dies");
        animator.SetBool("isDead", true);
    }

}
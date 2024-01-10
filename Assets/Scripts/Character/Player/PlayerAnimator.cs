using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public bool IsFacingRight = true;

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
            IsFacingRight = Mathf.Sign(player.InputX) > 0;
            spriteRenderer.flipX = !IsFacingRight;
        }
    }

    public void Jump()
    {
        animator.SetTrigger("Jump");
    }

    public void Tumble()
    {
        animator.SetTrigger("Tumble");
    }

    public void StartDeathSequence()
    {
        animator.SetTrigger("Dies");
        animator.SetBool("isDead", true);
    }

    public IEnumerator FlashWhite()
    {
        spriteRenderer.color = new Color(0.75f, 0.75f, 0.75f, 1.0f);
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = Color.white;
    }
}
using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

  internal SpriteRenderer _renderer;
  internal Animator _animator;
  internal PlayerController _player;

  private void Awake()
  {
    _renderer = GetComponent<SpriteRenderer>();
    _animator = GetComponent<Animator>();
    _player = GetComponent<PlayerController>();
  }

  private void Update()
  {
    if (GameManager.Paused) return;

    _animator.SetBool("Crouched", _player.Crouched);
    _animator.SetInteger("JumpCount", _player.JumpCount);
    _animator.SetBool("Grounded", _player.Grounded);
    _animator.SetFloat("VelocityX", _player.SpeedX);
    if (_player.InputX != 0)
    {
      _renderer.flipX = _player.InputX == -1;
    }
  }

  public void StartDeathSequence()
  {
    _animator.SetTrigger("Dies");
  }

}
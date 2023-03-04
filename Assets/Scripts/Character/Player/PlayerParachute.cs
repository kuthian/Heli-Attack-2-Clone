using UnityEngine;

public class PlayerParachute : MonoBehaviour {
  
  [SerializeField] private int ParachuteDrag = 10;
  internal SpriteRenderer _spriteRenderer;
  internal PlayerController _player;

  private void Start()
  {
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _player = GetComponentInParent<PlayerController>();
    DeployParachute();
  }

  public void DeployParachute()
  {
    if (_player) {
      _player.Rigidbody.drag = ParachuteDrag;
    } 
    if (_spriteRenderer) {
      _spriteRenderer.enabled = true;
    } 
  }

  private void OnEnable()
  {
    DeployParachute();
  }

  private void OnDisable()
  {
    _player.Rigidbody.drag = 0;
    _spriteRenderer.enabled = false;
  }

  void FixedUpdate()
  {
    if (_player.Grounded) {
      enabled = false;
    }
  }  

};
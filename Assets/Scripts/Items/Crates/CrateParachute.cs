using UnityEngine;

public class CrateParachute : MonoBehaviour {

  public int ParachuteDrag = 10;
  internal SpriteRenderer _spriteRenderer;
  internal CrateController _crate;

  private void Start()
  {
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _crate = GetComponentInParent<CrateController>();
    DeployParachute();
  }
  public void DeployParachute()
  {
    if (_crate) {
      _crate.Rigidbody.drag = ParachuteDrag;
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
    _crate.Rigidbody.drag = 0;
    _spriteRenderer.enabled = false;
  }

  void FixedUpdate()
  {
    if (_crate.Grounded) {
      enabled = false;
    }
  }  
}

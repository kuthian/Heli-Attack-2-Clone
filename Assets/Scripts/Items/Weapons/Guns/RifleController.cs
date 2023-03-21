using UnityEngine;

public class RifleController : __GunController {

  override public void SyncAnimation()
  { 
    _animator.Play("Rifle", -1, 0f);
  }

}
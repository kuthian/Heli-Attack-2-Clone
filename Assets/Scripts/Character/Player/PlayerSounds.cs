using UnityEngine;

public class PlayerSounds : MonoBehaviour {
  
  public void PlayFmodFootstepsEvent()
  {
    FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Footsteps", GetComponent<Transform>().position);
  }
  
  public void PlayFmodJumpEvent()
  {
    FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Jump", GetComponent<Transform>().position);
  }

}
using UnityEngine;

public class ShotgunSounds : MonoBehaviour {
  
  [SerializeField]
  private AK.Wwise.Event _wwShotgunLoad;

  [SerializeField]
  private AK.Wwise.Event _wwShotgunCock;

  public void ShotgunLoadSound()
  {
    if (_wwShotgunLoad.IsValid()) {
      _wwShotgunLoad.Post(gameObject);
    }  
  }
  public void ShotgunCockSound()
  {
    if (_wwShotgunCock.IsValid()) {
      _wwShotgunCock.Post(gameObject);
    }  
  }

}
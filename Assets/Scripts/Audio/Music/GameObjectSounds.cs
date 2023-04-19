using UnityEngine;

public class GameObjectSounds : MonoBehaviour {
  
  [SerializeField]
  private AK.Wwise.Event _wwOnStart;
  [SerializeField]
  private AK.Wwise.Event _wwOnDestroy;

  private void Start()
  {
    if (_wwOnStart.IsValid()) {
      _wwOnStart.Post(gameObject);
    }
  }

  private void OnDestroy()
  {
    if (_wwOnDestroy.IsValid()) {
      _wwOnDestroy.Post(gameObject);
    } 
  }
}
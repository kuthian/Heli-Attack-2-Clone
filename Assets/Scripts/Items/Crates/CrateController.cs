using UnityEngine;

public class CrateController : MonoBehaviour {

  private __CrateAction _action;

  public void Start()
  {
    _action = GetComponent<__CrateAction>();
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      AudioManager.PlayCrateOpen();
      _action.Do(other.gameObject);
      Destroy(gameObject);
    } 
  }
}
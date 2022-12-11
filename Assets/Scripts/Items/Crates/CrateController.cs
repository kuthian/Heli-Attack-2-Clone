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
      _action.Do(other.gameObject);
      Destroy(gameObject);
    } 
  }
}
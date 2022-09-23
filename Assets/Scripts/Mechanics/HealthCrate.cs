using UnityEngine;

public class HealthCrate : MonoBehaviour {
  
  [SerializeField] private int healBy = 20;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      AudioManager.PlayCrateOpen();
      other.gameObject.SendMessage("Heal", healBy);
      Destroy(gameObject);
    }
  }

}
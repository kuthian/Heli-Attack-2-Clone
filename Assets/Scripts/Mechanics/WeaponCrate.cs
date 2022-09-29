using UnityEngine;

public class WeaponCrate : MonoBehaviour {

  public GameObject weapon;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      AudioManager.PlayCrateOpen();
      other.gameObject.SendMessage("AddWeapon", weapon);
      Destroy(gameObject);
    } 
  }
}
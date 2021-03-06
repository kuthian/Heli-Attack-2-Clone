using UnityEngine;

public class WeaponCrate : MonoBehaviour {

  [SerializeField] private Transform weapon;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      other.gameObject.SendMessage("AddWeapon", weapon);
      Destroy(gameObject);
    } 
  }
}
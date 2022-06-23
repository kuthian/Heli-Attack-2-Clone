using UnityEngine;

public class InventoryController : MonoBehaviour {

  public void AddWeapon( Transform weapon ) 
  {
    bool firstWeapon = transform.childCount == 0;
    var obj = Instantiate(weapon, transform);
    if (!firstWeapon) obj.gameObject.SetActive(false);
  }
  
  private void Update()
  {
    if (Input.GetKeyDown("q"))
    {
      SelectPreviousWeapon();
    }
    if (Input.GetKeyDown("e"))
    {
      SelectNextWeapon();
    }
  }
  
  private void SelectNextWeapon()
  {
    if ( transform.childCount > 1 )
    {
      int index = SelectedWeaponIndex();
      transform.GetChild(index).gameObject.SetActive(false);
      if (index+1 == transform.childCount)
      {
        // Last weapon, loop back to start of inventory
        transform.GetChild(0).gameObject.SetActive(true);
      } 
      else
      {
        // Move forward in inventory
        transform.GetChild(index+1).gameObject.SetActive(true);
      } 
    }
  }
  
  private void SelectPreviousWeapon()
  {
    if ( transform.childCount > 1 )
    {
      int index = SelectedWeaponIndex();
      transform.GetChild(index).gameObject.SetActive(false);
      if (index == 0)
      {
        // Last weapon, loop back to end of inventory
        transform.GetChild(transform.childCount-1).gameObject.SetActive(true);
      } 
      else
      {
        // Move backwards in inventory
        transform.GetChild(index-1).gameObject.SetActive(true);
      } 
    }

  }

  private int SelectedWeaponIndex()
  {
    int index = -1;
    foreach (Transform child in transform) {
      if (child.gameObject.activeSelf) {
        index = child.GetSiblingIndex();
      }
    }
    return index;
  }
}
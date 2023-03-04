using UnityEngine;

public class InventoryController : MonoBehaviour {

  static int NoWeaponSelected = -99;

  public void AddWeapon( GameObject weapon ) 
  {
    // Check if weapon already exists.
    // If it does, increase its ammo count
    foreach (Transform child in transform) {
      if (child.gameObject.name == string.Format("{0}(Clone)", weapon.name)) {
        var ammo = child.gameObject.GetComponent<Ammo>();
        ammo.Add( ammo.StartingCount );
        return;
      }
    }

    // Weapon doesn't already exist, add it
    bool firstWeapon = transform.childCount == 0;
    GameObject obj = Instantiate(weapon, transform) as GameObject;
    if (firstWeapon) obj.SetActive(true);
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
    if (Input.mouseScrollDelta.y > 0)
    {
      SelectNextWeapon();
    }
    if (Input.mouseScrollDelta.y < 0)
    {
      SelectPreviousWeapon();
    }

    CheckActiveWeaponAmmo();
  }

  private void CheckActiveWeaponAmmo()
  {
    int index = SelectedWeaponIndex();
    if (NoWeaponSelected == index) return;
    var activeWeapon = transform.GetChild(index).gameObject;
    if( activeWeapon.GetComponent<Ammo>().Empty() )
    {
      SelectNextWeapon();
      Destroy(activeWeapon);
    }
  }

  private void SelectWeapon( GameObject weapon )
  {
    int index = SelectedWeaponIndex();
    if ( NoWeaponSelected != index ) {
      transform.GetChild(index).gameObject.SetActive(false);
    }
    weapon.SetActive(true);
    HUDManager.Inventory.SetActiveWeapon( weapon.GetComponent<__GunController>().InventorySprite );
    HUDManager.Inventory.SetAmmoCount( weapon.GetComponent<Ammo>().Count );
    weapon.GetComponent<Ammo>().OnAmmoChanged += HUDManager.Inventory.SetAmmoCount;
  }

  private void SelectNextWeapon()
  {
    if ( transform.childCount > 1 )
    {
      int index = SelectedWeaponIndex();
      if (index+1 == transform.childCount)
      {
        // Last weapon, loop back to start of inventory
        SelectWeapon( transform.GetChild(0).gameObject );
      } 
      else
      {
        // Move forward in inventory
        SelectWeapon( transform.GetChild(index+1).gameObject );
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
        // First weapon, loop back to end of inventory
        SelectWeapon( transform.GetChild(transform.childCount-1).gameObject );
      }
      else
      {
        // Move backwards in inventory
        SelectWeapon( transform.GetChild(index-1).gameObject );
      } 
    }

  }

  private int SelectedWeaponIndex()
  {
    int index = NoWeaponSelected;
    foreach (Transform child in transform) {
      if (child.gameObject.activeSelf) {
        index = child.GetSiblingIndex();
      }
    }
    return index;
  }

  public __GunController GunController()
  {
    foreach (Transform child in transform) {
      if (child.gameObject.activeSelf) {
        return child.gameObject.GetComponent<__GunController>();
      }
    }
    return null;
  }
}
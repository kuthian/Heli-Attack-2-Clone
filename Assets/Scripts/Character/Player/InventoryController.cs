using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    public InputAction nextWeapon;
    public InputAction previousWeapon;
    public InputAction scrollAction;
    public Transform weapons;

    private static int NoWeaponSelected = -99;

    public void AddWeapon(GameObject weapon)
    {
        // Check if weapon already exists.
        // If it does, increase its ammo count
        foreach (Transform child in weapons)
        {
            if (child.gameObject.name == string.Format("{0}(Clone)", weapon.name))
            {
                var ammo = child.gameObject.GetComponent<Ammo>();
                ammo.Add(ammo.StartingCount);
                return;
            }
        }

        // Weapon doesn't already exist, add it
        bool firstWeapon = weapons.childCount == 0;
        GameObject obj = Instantiate(weapon, weapons) as GameObject;
        obj.SetActive(firstWeapon);
    }

    public void HideWeapon()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        CheckActiveWeaponAmmo();
    }

    private void CheckActiveWeaponAmmo()
    {
        int index = SelectedWeaponIndex();
        if (NoWeaponSelected == index) return;
        var activeWeapon = weapons.GetChild(index).gameObject;
        if (activeWeapon.GetComponent<Ammo>().Empty())
        {
            SelectDefaultWeapon();
            Destroy(activeWeapon);
        }
    }

    private void SelectWeapon(GameObject weapon)
    {
        int index = SelectedWeaponIndex();
        if (NoWeaponSelected != index)
        {
            var active_weapon = weapons.GetChild(index).gameObject;
            active_weapon.SetActive(false);
            active_weapon.GetComponent<Ammo>().OnAmmoChanged -= HUDManager.Inventory.SetAmmoCount;
        }
        weapon.SetActive(true);
        HUDManager.Inventory.SetActiveWeapon(weapon.GetComponent<__GunController>().InventorySprite);
        HUDManager.Inventory.SetAmmoCount(weapon.GetComponent<Ammo>().Count);
        weapon.GetComponent<Ammo>().OnAmmoChanged += HUDManager.Inventory.SetAmmoCount;
    }

    private void SelectDefaultWeapon()
    {
        SelectWeapon(weapons.GetChild(0).gameObject);
    }

    private void SelectNextWeapon()
    {
        if (weapons.gameObject.activeSelf && weapons.childCount > 1)
        {
            int index = SelectedWeaponIndex();
            if (index + 1 == weapons.childCount)
            {
                // Last weapon, loop back to start of inventory
                SelectWeapon(weapons.GetChild(0).gameObject);
            }
            else
            {
                // Move forward in inventory
                SelectWeapon(weapons.GetChild(index + 1).gameObject);
            }
        }
    }

    private void SelectPreviousWeapon()
    {
        if (weapons.gameObject.activeSelf && weapons.childCount > 1)
        {
            int index = SelectedWeaponIndex();
            weapons.GetChild(index).gameObject.SetActive(false);
            if (index == 0)
            {
                // First weapon, loop back to end of inventory
                SelectWeapon(weapons.GetChild(weapons.childCount - 1).gameObject);
            }
            else
            {
                // Move backwards in inventory
                SelectWeapon(weapons.GetChild(index - 1).gameObject);
            }
        }

    }

    private int SelectedWeaponIndex()
    {
        int index = NoWeaponSelected;
        foreach (Transform child in weapons)
        {
            if (child.gameObject.activeSelf)
            {
                index = child.GetSiblingIndex();
            }
        }
        return index;
    }

    private void OnScroll(float scroll)
    {
        if (scroll > 0)
        {
            // Scrolling up
            SelectNextWeapon();
        }
        else if (scroll < 0)
        {
            // Scrolling down
            SelectPreviousWeapon();
        }
    }

    private void OnEnable()
    {
        scrollAction.Enable();
        scrollAction.performed += context => OnScroll(context.ReadValue<float>());

        nextWeapon.Enable();
        nextWeapon.performed += _ => SelectNextWeapon();

        previousWeapon.Enable();
        previousWeapon.performed += _ => SelectPreviousWeapon();

    }

    private void OnDisable()
    {
        scrollAction.Disable();
        scrollAction.performed -= context => OnScroll(context.ReadValue<float>());

        nextWeapon.Disable();
        nextWeapon.performed -= _ => SelectNextWeapon();

        previousWeapon.Disable();
        previousWeapon.performed -= _ => SelectPreviousWeapon();
    }

}
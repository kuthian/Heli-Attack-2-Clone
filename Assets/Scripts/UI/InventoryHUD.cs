using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHUD : MonoBehaviour
{
    private static int INFINITE_AMMO = -1;

    internal Image activeWeapon;
    internal TextMeshProUGUI ammoText;

    public void Start()
    {
        activeWeapon = GetComponentInChildren<Image>();
        ammoText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetActiveWeapon(Sprite sprite)
    {
        activeWeapon.sprite = sprite;
    }

    public void SetAmmoCount(int ammoCount)
    {
        if (ammoCount == INFINITE_AMMO)
        {
            ammoText.fontSize = 70;
            ammoText.SetText("\u221E"); // infinity symbol
        }
        else
        {
            ammoText.fontSize = 50;
            ammoText.SetText(ammoCount.ToString());
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHUD : MonoBehaviour {

  internal Image _activeWeapon;
  internal TextMeshProUGUI _ammoText;
  private int _InfiniteAmmo = -1;
  private string _ammoSuffix = " x";

  public void Start()
  {
    _activeWeapon = GetComponentInChildren<Image>();
    _ammoText = GetComponentInChildren<TextMeshProUGUI>();
  }

  public void SetActiveWeapon( Sprite sprite )
  {
    _activeWeapon.sprite = sprite;
  }

  public void SetAmmoCount( int ammoCount )
  {
    _ammoText.SetText(GenerateAmmoText(ammoCount));
  }

  private string GenerateAmmoText(int ammoCount)
  {
    string str;
    if (_InfiniteAmmo == ammoCount) str = "Infinite" + _ammoSuffix;
    else str = ammoCount + _ammoSuffix;
    return str;
  }

}
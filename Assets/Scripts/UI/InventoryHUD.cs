using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHUD : MonoBehaviour {

  internal Image _activeWeapon;
  internal TextMeshProUGUI _ammoText;
  private int _InfiniteAmmo = -1;

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
    if (_InfiniteAmmo == ammoCount)
    {
      _ammoText.fontSize = 70;
      _ammoText.SetText("\u221E");
    }
    else
    {
      _ammoText.fontSize = 50;
      _ammoText.SetText(ammoCount.ToString());
    }
  }
}
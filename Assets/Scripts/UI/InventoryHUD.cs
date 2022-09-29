using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHUD : MonoBehaviour {

  [SerializeField] private Image _activeWeapon;
  [SerializeField] private TextMeshProUGUI _ammoText;
  private int _InfiniteAmmo = -1;
  private string _ammoSuffix = " x";

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
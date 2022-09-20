using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHUD : MonoBehaviour {

  [SerializeField] private Image activeWeapon;
  [SerializeField] private TextMeshProUGUI ammoText;
  private int InfiniteAmmo = -1;
  private string ammoSuffix = " x";
  
  public void SetActiveWeapon( Sprite sprite )
  {
    activeWeapon.sprite = sprite;
  }

  public void SetAmmoCount( int ammoCount )
  {
    ammoText.SetText(GenerateAmmoText(ammoCount));
  }

  private string GenerateAmmoText(int ammoCount)
  {
    string str;
    if (InfiniteAmmo == ammoCount) str = "Infinite" + ammoSuffix;
    else str = ammoCount + ammoSuffix;
    return str;
  }

}
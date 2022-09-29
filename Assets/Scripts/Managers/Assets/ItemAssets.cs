using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GunItem
{
  public GunType type;
  public Sprite crateSprite;
  public GameObject gunPrefab;
}

public class ItemAssets : MonoBehaviour {
  
  private static ItemAssets _i;

  public static ItemAssets i {
    get {
      if (_i == null) _i = Instantiate(Resources.Load<ItemAssets>("ItemAssets"));
      return _i;
    }
  }

  [SerializeField] private GunItem[] _Guns;

  public GunItem[] GunItems => _Guns;

}
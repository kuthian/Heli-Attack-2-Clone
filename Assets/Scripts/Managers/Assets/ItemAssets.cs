using UnityEngine;

public class ItemAssets : MonoBehaviour {
  
  private static ItemAssets _i;

  public static ItemAssets i {
    get {
      _i = (ItemAssets) FindObjectOfType(typeof(ItemAssets));
      if (_i == null) {
        _i = Instantiate(Resources.Load<ItemAssets>("ItemAssets"));
      }
      return _i;
    }
  }

  [SerializeField] private GameObject _riflePrefab;

  public GameObject RiflePrefab => _riflePrefab;

  [SerializeField] private GameObject _uziPrefab;

  public GameObject UziPrefab => _uziPrefab;

  [SerializeField] private GameObject _shotgunPrefab;

  public GameObject ShotgunPrefab => _shotgunPrefab;

  [SerializeField] private GameObject[] _cratePrefabs;

  public GameObject[] CratePrefabs => _cratePrefabs;

}
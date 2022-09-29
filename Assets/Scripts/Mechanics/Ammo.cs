using UnityEngine;

public class Ammo : MonoBehaviour {

  public bool _infiniteAmmo = false;

  [SerializeField] private int _startingAmmo = 10;
  private int _ammoCount;

  public int StartingCount => _startingAmmo;
  public int Count => _ammoCount;

  public delegate void _OnAmmoChanged(int Ammo);
  public event _OnAmmoChanged OnAmmoChanged;

  public delegate void _OnAmmoZero();
  public event _OnAmmoZero OnAmmoZero;

  void Awake()
  {
    _ammoCount = _infiniteAmmo ? -1 : _startingAmmo;
  }

  public bool Empty()
  {
    return !_infiniteAmmo && _ammoCount <= 0;
  }

  public void Add( int count )
  {
    if (!_infiniteAmmo)
    {
      _ammoCount += count;
      OnAmmoChanged?.Invoke(_ammoCount);
    }
  }

  public void Remove( int count )
  {
    if (!_infiniteAmmo)
    {
      _ammoCount -= count;
      if (_ammoCount < 0) _ammoCount = 0;
      if (_ammoCount == 0) OnAmmoZero?.Invoke();
      OnAmmoChanged?.Invoke(_ammoCount);
    }
  }

}
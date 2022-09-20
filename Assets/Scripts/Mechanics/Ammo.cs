using UnityEngine;

public class Ammo : MonoBehaviour {

  public bool infiniteAmmo = false;

  [SerializeField] private int startingAmmo = 10;
  private int ammoCount;

  public int StartingCount => startingAmmo;
  public int Count => ammoCount;

  public delegate void _OnAmmoChanged(int Ammo);
  public event _OnAmmoChanged OnAmmoChanged;

  public delegate void _OnAmmoZero();
  public event _OnAmmoZero OnAmmoZero;

  void Awake()
  {
    ammoCount = infiniteAmmo ? -1 : startingAmmo;
  }

  public bool Empty()
  {
    return !infiniteAmmo && ammoCount <= 0;
  }

  public void Add( int count )
  {
    if (!infiniteAmmo)
    {
      ammoCount += count;
      OnAmmoChanged?.Invoke(ammoCount);
    }
  }

  public void Remove( int count )
  {
    if (!infiniteAmmo)
    {
      ammoCount -= count;
      if (ammoCount < 0) ammoCount = 0;
      if (ammoCount == 0) OnAmmoZero?.Invoke();
      OnAmmoChanged?.Invoke(ammoCount);
    }
  }

}
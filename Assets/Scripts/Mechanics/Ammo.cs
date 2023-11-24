using UnityEngine;

public class Ammo : MonoBehaviour
{
    public bool _infiniteAmmo = false;

    [SerializeField] private int _startingAmmo = 10;

    public int StartingCount => _startingAmmo;
    public int Count { get; private set;  }

    public delegate void _OnAmmoChanged(int Ammo);
    public event _OnAmmoChanged OnAmmoChanged;

    public delegate void _OnAmmoZero();
    public event _OnAmmoZero OnAmmoZero;

    void Awake()
    {
        Count = _infiniteAmmo ? -1 : _startingAmmo;
    }

    public bool Empty()
    {
        return !_infiniteAmmo && Count <= 0;
    }

    public void Add(int count)
    {
        if (!_infiniteAmmo)
        {
            Count += count;
            OnAmmoChanged?.Invoke(Count);
        }
    }

    public void Remove(int count)
    {
        if (!_infiniteAmmo)
        {
            Count -= count;
            if (Count < 0) Count = 0;
            if (Count == 0) OnAmmoZero?.Invoke();
            OnAmmoChanged?.Invoke(Count);
        }
    }

}
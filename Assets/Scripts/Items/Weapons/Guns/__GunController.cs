using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class __GunController : MonoBehaviour {

  internal Ammo _ammo;
  internal Animator _animator;

  [field:SerializeField] 
  public Sprite InventorySprite { get; set; }

  [SerializeField]
  protected Transform _pfProjectile;

  protected Transform _firePointTransform;

  [SerializeField] private float _cooldownTime = 0.5f;
  [SerializeField] protected int _ammoPerShot = 1;
  private DateTime _cooldownOffTime;
  private float _cooldownTimeRemaining;
  private bool _onCooldown = false;
  private bool _shoot = false;
  public bool Steady { get; set; } = false;

  virtual public void SyncAnimation() {}

  private void Awake()
  {
    _animator = GetComponent<Animator>();
    _firePointTransform = transform.Find("FirePoint");
    _ammo = GetComponent<Ammo>();
    _cooldownTimeRemaining = 0;
  }

  private void OnEnable()
  {
    HUDManager.ReloadBar.SetCooldownTime(_cooldownTime);
    HUDManager.ReloadBar.SetTimeRemaining(0);
  }

  private void OnDisable()
  {
    _shoot = false;
  }

  virtual protected void InstantiateProjectile()
  {
    Transform projectileTransform = 
      Instantiate(_pfProjectile, _firePointTransform.position, Quaternion.identity, DynamicObjects.Projectiles);

    Vector3 direction = _firePointTransform.right;

    projectileTransform.GetComponent<Projectile>().Setup(direction);
  }

  private void Update()
  {
    if (GameManager.Paused) return;

    if (_onCooldown)
    {
      float timeRemaining = (float)(_cooldownOffTime - DateTime.Now).TotalSeconds;
      if (timeRemaining < 0)
      {
        _onCooldown = false;
        timeRemaining = 0;
      }
      HUDManager.ReloadBar.SetTimeRemaining(timeRemaining);
    }

    if (Input.GetMouseButtonDown(0))
    {
      _shoot = true;
    }
    if (Input.GetMouseButtonUp(0))
    {
      _shoot = false;
    }

    if (!_onCooldown && _shoot && !_ammo.Empty())
    {
      InstantiateProjectile();

      _cooldownOffTime = DateTime.Now.AddSeconds(_cooldownTime);
      _cooldownTimeRemaining = _cooldownTime;
      _onCooldown = true;

      _ammo.Remove(_ammoPerShot);
    }

  }

  private void FixedUpdate()
  {
    if (_onCooldown)
    {
      _cooldownTimeRemaining -= Time.fixedDeltaTime;
    }
  }
}

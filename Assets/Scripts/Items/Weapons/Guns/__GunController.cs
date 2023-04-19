using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class __GunController : MonoBehaviour {

  [SerializeField]
  protected AK.Wwise.Event _wwOnShoot;

  [field:SerializeField] 
  public Sprite InventorySprite { get; set; }

  public bool Steady { get; set; } = false;

  internal protected Ammo _ammo;
  internal protected Animator _animator;

  [Serializable]
  public struct projectile {
    public Transform prefab;
    public float damage;
    public float speed;
    public float maxLifetime;
  }

  [SerializeField]
  protected projectile _projectile;
  protected Transform _firePointTransform;

  [SerializeField] protected int _ammoPerShot = 1;
  [SerializeField] private float _cooldownTime = 0.5f;
  private DateTime _cooldownOffTime;
  private float _cooldownTimeRemaining;
  private bool _onCooldown = false;
  private bool _shoot = false;

  virtual protected void OnShootStart() {}
  virtual protected void OnShootEnd() {}
  virtual protected void OnAmmoEmpty() {}

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
    _animator.SetBool("OnCooldown", _onCooldown);
  }

  private void OnDisable()
  {
    _shoot = false;
  }

  virtual protected void Shoot()
  {
    InstantiateProjectile( _firePointTransform.position, _firePointTransform.right);
    if (_wwOnShoot.IsValid()) {
      _wwOnShoot.Post(gameObject);
    }
  }

  virtual protected void InstantiateProjectile( Vector3 position, Vector3 direction )
  {
    if ( ! _projectile.prefab ) return;

    Transform projectile = 
      Instantiate(_projectile.prefab, position, Quaternion.identity, DynamicObjects.Projectiles);

    projectile.GetComponent<Projectile>().Damage = _projectile.damage;
    projectile.GetComponent<Projectile>().Speed = _projectile.speed;
    projectile.GetComponent<Projectile>().MaxLifetimeSeconds = _projectile.maxLifetime;
    projectile.GetComponent<Projectile>().Shoot(direction);
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
        _animator.SetBool("OnCooldown", _onCooldown);
        timeRemaining = 0;
      }
      HUDManager.ReloadBar.SetTimeRemaining(timeRemaining);
    }

    if (Input.GetMouseButtonDown(0))
    {
      _shoot = true;
      OnShootStart();
    }
    if (Input.GetMouseButtonUp(0))
    {
      _shoot = false;
      OnShootEnd();
    }

    if (!_onCooldown && _shoot && !_ammo.Empty())
    {
      Shoot();

      _cooldownOffTime = DateTime.Now.AddSeconds(_cooldownTime);
      _cooldownTimeRemaining = _cooldownTime;
      _animator.SetBool("OnCooldown", true);
      _onCooldown = true;

      _ammo.Remove(_ammoPerShot);
      if (_ammo.Empty())
      {
        OnAmmoEmpty();
      }
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

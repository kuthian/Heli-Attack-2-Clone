using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class __GunController : MonoBehaviour
{
    [SerializeField]
    protected AK.Wwise.Event _wwOnShoot;

    [field: SerializeField]
    public Sprite InventorySprite { get; set; }

    public bool ShootingDisabled { get; set; } = false;

    public InputAction shootAction;

    internal protected Ammo ammo;
    internal protected Animator animator;

    [Serializable]
    public struct projectile
    {
        public Transform prefab;
        public float damage;
        public float speed;
        public float maxLifetime;
    }

    [SerializeField]
    protected projectile _projectile;
    protected Transform firePointTransform;

    [SerializeField] protected int _ammoPerShot = 1;
    [SerializeField] private float _cooldownTime = 0.5f;
    private DateTime cooldownOffTime;
    private bool onCooldown = false;
    private bool shoot = false;

    virtual protected void OnShootStart() { }
    virtual protected void OnShootEnd() { }
    virtual protected void OnAmmoEmpty() { }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        firePointTransform = transform.Find("FirePoint");
        ammo = GetComponent<Ammo>();
    }

    private void OnEnable()
    {
        shootAction.performed += ShootStart;
        shootAction.canceled += ShootEnd;
        shootAction.Enable();

        HUDManager.ReloadBar.SetCooldownTime(_cooldownTime);
        HUDManager.ReloadBar.SetTimeRemaining(0);
        AnimatorSetBool("OnCooldown", onCooldown);
    }

    private void OnDisable()
    {
        shootAction.performed -= ShootStart;
        shootAction.canceled -= ShootEnd;
        shootAction.Disable();
        shoot = false;
    }

    virtual protected void Shoot()
    {
        InstantiateProjectile(firePointTransform.position, firePointTransform.right);
        if (_wwOnShoot.IsValid())
        {
            _wwOnShoot.Post(gameObject);
        }
    }

    virtual protected void InstantiateProjectile(Vector3 position, Vector3 direction)
    {
        if (!_projectile.prefab) return;

        Transform projectile =
          Instantiate(_projectile.prefab, position, Quaternion.identity, DynamicObjects.Projectiles);

        projectile.GetComponent<Projectile>().Damage = _projectile.damage;
        projectile.GetComponent<Projectile>().Speed = _projectile.speed;
        projectile.GetComponent<Projectile>().MaxLifetimeSeconds = _projectile.maxLifetime;
        projectile.GetComponent<Projectile>().Shoot(direction);

        StatsManager.RegisterBulletFired();
    }

    private void Update()
    {
        if (GameManager.Paused) return;

        if (onCooldown)
        {
            float timeRemaining = (float)(cooldownOffTime - DateTime.Now).TotalSeconds;
            if (timeRemaining < 0)
            {
                onCooldown = false;
                AnimatorSetBool("OnCooldown", false);
                timeRemaining = 0;
            }
            HUDManager.ReloadBar.SetTimeRemaining(timeRemaining);
        }

        if (!onCooldown && shoot && !ammo.Empty())
        {
            Shoot();

            cooldownOffTime = DateTime.Now.AddSeconds(_cooldownTime);
            onCooldown = true;
            AnimatorSetBool("OnCooldown", true);

            ammo.Remove(_ammoPerShot);
            if (ammo.Empty())
            {
                OnAmmoEmpty();
            }
        }
    }

    private void AnimatorSetBool(string name, bool value)
    {
        if (animator.runtimeAnimatorController)
        {
            animator.SetBool(name, value);
        }
    }

    private void ShootStart(InputAction.CallbackContext context)
    {
        shoot = !ShootingDisabled;
        OnShootStart();
    }

    private void ShootEnd(InputAction.CallbackContext context)
    {
        shoot = false;
        OnShootEnd();
    }



}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class __GunController : MonoBehaviour
{
    [SerializeField]
    protected AK.Wwise.Event wwOnShoot;

    [field: SerializeField]
    public Sprite InventorySprite { get; set; }

    [field: SerializeField]
    public bool ShootingDisabled { get; set; } = false;

    public InputAction shootAction;

    internal protected Ammo ammo;
    internal protected Animator animator;
    internal protected SpriteRenderer spriteRenderer;

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

    public void Hide()
    {
        spriteRenderer.color = Color.clear;
        ShootingDisabled = true;
    }

    public void Show()
    {
        spriteRenderer.color = Color.white;
        ShootingDisabled = false;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        firePointTransform = transform.Find("FirePoint");
        ammo = GetComponent<Ammo>();
    }

    private void OnEnable()
    {
        shootAction.performed += ShootStart;
        shootAction.canceled += ShootEnd;
        shootAction.Enable();
        shoot = false;

        HUDManager.ReloadBar.SetCooldownTime(_cooldownTime);
        HUDManager.ReloadBar.SetTimeRemaining(0);
        AnimatorSetBool("OnCooldown", onCooldown);
    }

    private void OnDisable()
    {
        shootAction.performed -= ShootStart;
        shootAction.canceled -= ShootEnd;
        shootAction.Disable();
        OnShootEnd();
    }

    virtual protected void Shoot()
    {
        InstantiateProjectile(firePointTransform.position, firePointTransform.right);
        if (wwOnShoot.IsValid())
        {
            wwOnShoot.Post(gameObject);
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
        if (GameManager.Paused)
        {
            if (shoot)
            {
                shoot = false;
                OnShootEnd();
            }
            return;
        }

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
            return; // return early to let animators handle the OnCooldown false state
        }

        if (!ShootingDisabled && !onCooldown && shoot && !ammo.Empty())
        {
            Shoot();

            cooldownOffTime = DateTime.Now.AddSeconds(_cooldownTime);
            onCooldown = true;
            AnimatorSetBool("OnCooldown", true);
            AnimatorSetTrigger("Reload");

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
    private void AnimatorSetTrigger(string name)
    {
        if (animator.runtimeAnimatorController)
        {
            animator.SetTrigger(name);
        }
    }

    private void ShootStart(InputAction.CallbackContext context)
    {
        shoot = !ShootingDisabled;
        if (shoot) OnShootStart();
    }

    private void ShootEnd(InputAction.CallbackContext context)
    {
        shoot = false;
        OnShootEnd();
    }



}

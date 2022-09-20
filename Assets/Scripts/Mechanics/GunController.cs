using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

  public ReloadBar reloadBar;

  internal AudioSource shootingSound;
  internal Ammo ammo;

  [SerializeField] protected Transform firePointTransform;
  [SerializeField] protected Transform pfProjectile;
  [SerializeField] private Sprite crateSprite;

  [SerializeField] private float cooldownTime = 0.5f;
  [SerializeField] protected int ammoPerShot = 1;
  private DateTime cooldownOffTime;
  private float cooldownTimeRemaining;
  private bool onCooldown = false;
  private bool shoot = false;

  public Sprite CrateSprite => crateSprite;

  public void Init(ReloadBar rBar)
  {
    reloadBar = rBar;
  }

  private void Awake()
  {
    shootingSound = GetComponent<AudioSource>();
    ammo = GetComponent<Ammo>();
    cooldownTimeRemaining = 0;
  }

  private void OnEnable()
  {
    reloadBar.SetCooldownTime(cooldownTime);
    reloadBar.SetTimeRemaining(0);
  }

  private void OnDisable()
  {
    shoot = false;
  }

  virtual protected void InstantiateProjectile()
  {
    Transform projectileTransform = 
      Instantiate(pfProjectile, firePointTransform.position, Quaternion.identity);

    Vector3 direction = firePointTransform.right;

    projectileTransform.GetComponent<Projectile>().Setup(direction);
  }

  private void Update()
  {
    if (onCooldown)
    {
      float timeRemaining = (float)(cooldownOffTime - DateTime.Now).TotalSeconds;
      if (timeRemaining < 0)
      {
        onCooldown = false;
        timeRemaining = 0;
      }
      reloadBar.SetTimeRemaining(timeRemaining);
    }

    if (Input.GetMouseButtonDown(0))
    {
      shoot = true;
    }
    if (Input.GetMouseButtonUp(0))
    {
      shoot = false;
    }

    if (!onCooldown && shoot && !ammo.Empty())
    {
      InstantiateProjectile();

      shootingSound.Play(0);

      cooldownOffTime = DateTime.Now.AddSeconds(cooldownTime);
      cooldownTimeRemaining = cooldownTime;
      onCooldown = true;

      ammo.Remove(ammoPerShot);
    }

  }

  private void FixedUpdate()
  {
    if (onCooldown)
    {
      cooldownTimeRemaining -= Time.fixedDeltaTime;
    }
  }
}

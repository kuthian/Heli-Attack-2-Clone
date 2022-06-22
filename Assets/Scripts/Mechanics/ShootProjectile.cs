using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
  [SerializeField] private Transform firePointTransform;
  [SerializeField] private Transform pfProjectile;

  public float shootCooldownSec = 0.5f;
  private bool onCooldown = false;
  private bool shoot = false;

  private void Awake()
  {
    GetComponent<AimAtMouse>().OnClick += ShootProjectile_OnClick;
    GetComponent<AimAtMouse>().OnClickReleased += ShootProjectile_OnClickReleased;
  }

  public void ShootProjectile_OnClick(object sender, AimAtMouse.OnClickEventArgs e)
  {
    shoot = true;
  }

  public void ShootProjectile_OnClickReleased(object sender, EventArgs e)
  {
    shoot = false;
  }
  
  private void Update()
  {
    if (shoot && !onCooldown)
    {
      Transform projectileTransform = 
        Instantiate(pfProjectile, firePointTransform.position, Quaternion.identity);

      Vector3 direction = firePointTransform.right;

      projectileTransform.GetComponent<Projectile>().Setup(direction);

      StartCoroutine(ShootCooldown());
    }
  }

  private IEnumerator ShootCooldown() {
    onCooldown = true;
    yield return new WaitForSeconds(shootCooldownSec);
    onCooldown = false;
  }
}

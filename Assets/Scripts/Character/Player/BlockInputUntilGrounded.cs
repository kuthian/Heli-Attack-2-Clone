using UnityEngine;

public class BlockInputUntilGrounded : MonoBehaviour
{
    internal PlayerController player;
    internal __GunController gun;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
        gun = GetComponentInChildren<__GunController>();
    }

    void Start()
    {
        gun.ShootingDisabled = true;
        player.BlockInput = true;
    }

    void Update()
    {
        if (player.Grounded)
        {
            player.BlockInput = false;
            gun.ShootingDisabled = false;
            enabled = false;
        }
    }

}
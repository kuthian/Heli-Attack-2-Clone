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
        player.BlockInput();
    }

    void Update()
    {
        if (player.Grounded)
        {
            player.EnableInput();
            gun.ShootingDisabled = false;
            enabled = false;
        }
    }

}
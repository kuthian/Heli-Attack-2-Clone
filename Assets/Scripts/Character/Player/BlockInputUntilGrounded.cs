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
        gun.gameObject.SetActive(false);
        player.BlockInput();
    }

    void Update()
    {
        if (player.Grounded)
        {
            Debug.Log("Grounded");
            player.EnableInput();
            gun.gameObject.SetActive(true);
            gun.ShootingDisabled = false;
            enabled = false;
        }
    }

}
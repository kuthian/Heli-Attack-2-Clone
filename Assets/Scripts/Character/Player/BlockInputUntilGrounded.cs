using UnityEngine;

public class BlockInputUntilGrounded : MonoBehaviour
{
    internal PlayerController player;

    void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    void Start()
    {
        player.BlockInput = true;
    }

    void Update()
    {
        if (player.Grounded)
        {
            player.BlockInput = false;
            enabled = false;
        }
    }

}
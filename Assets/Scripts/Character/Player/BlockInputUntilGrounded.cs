using UnityEngine;

public class BlockInputUntilGrounded : MonoBehaviour
{
    internal PlayerController player;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    void Start()
    {
        player.BlockInput();
    }

    void Update()
    {
        if (player.Grounded)
        {
            player.EnableInput();
            enabled = false;
        }
    }

}
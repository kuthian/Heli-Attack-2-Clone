using UnityEngine;

public class Translate: MonoBehaviour
{
    // Speed of movement in each direction
    [Header("Speeds")]
    [Range(-10, 10)]
    public float speedX = 0f;
    [Range(-10, 10)]
    public float speedY = 0f;
    [Range(-10, 10)]
    public float speedZ = 0f;

    void LateUpdate()
    {
        Vector3 pos = transform.position;

        // Calculate new position for each axis
        float newX = pos.x + speedX * Time.deltaTime;
        float newY = pos.y + speedY * Time.deltaTime;
        float newZ = pos.z + speedZ * Time.deltaTime;

        // Update the position of the GameObject
        transform.position = new Vector3(newX, newY, newZ);
    }
}
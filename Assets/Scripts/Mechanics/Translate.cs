using UnityEngine;

public class Translate: MonoBehaviour
{
    // Speed of movement in each direction
    [Header("Speeds")]
    [Range(0, 10)]
    public float speedX = 5f;
    [Range(0, 10)]
    public float speedY = 5f;
    [Range(0, 10)]
    public float speedZ = 5f;

    private Vector3 startPosition;
    private float elapsedTime = 0f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        // Calculate new position for each axis
        float newX = startPosition.x + speedX * elapsedTime;
        float newY = startPosition.y + speedY * elapsedTime;
        float newZ = startPosition.z + speedZ * elapsedTime;

        // Update the position of the GameObject
        transform.position = new Vector3(newX, newY, newZ);
    }
}
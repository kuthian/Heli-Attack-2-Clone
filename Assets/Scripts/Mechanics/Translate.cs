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

    public bool reset = false;

    private Vector3 startPosition;
    private float elapsedTime = 0f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (reset)
        {
            transform.position = startPosition;
            reset = false;
            return;
        }

        elapsedTime += Time.deltaTime;

        // Calculate new position for each axis
        float newX = startPosition.x + speedX * elapsedTime;
        float newY = startPosition.y + speedY * elapsedTime;
        float newZ = startPosition.z + speedZ * elapsedTime;

        // Update the position of the GameObject
        transform.position = new Vector3(newX, newY, newZ);
    }
}
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    // Amplitude for movement in each direction
    public float amplitudeX = 5f;
    public float amplitudeY = 5f;
    public float amplitudeZ = 5f;

    // Frequency for movement in each direction
    public float frequencyX = 1f;
    public float frequencyY = 1f;
    public float frequencyZ = 1f;

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
        float newX = startPosition.x + Mathf.Sin(elapsedTime * frequencyX) * amplitudeX;
        float newY = startPosition.y + Mathf.Sin(elapsedTime * frequencyY) * amplitudeY;
        float newZ = startPosition.z + Mathf.Sin(elapsedTime * frequencyZ) * amplitudeZ;

        // Update the position of the camera
        transform.position = new Vector3(newX, newY, newZ);
    }
}
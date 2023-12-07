using UnityEngine;

/// <summary>
/// The ParallaxEffect class creates a parallax movement effect for a GameObject.
/// It adjusts the GameObject's position based on the camera's movement and the specified
/// subject's position, giving a sense of depth in a 2D environment.
/// </summary>
public class ParallaxEffect : MonoBehaviour
{
    [Tooltip("Reference to the subject (e.g. player) that influences the parallax effect.")]
    public Transform subject;

    [Tooltip("Enable smoothing.")]
    public bool useSmoothing = false;

    [Tooltip("Enable parallax in the X direction.")]
    public bool parallaxX = true;

    [Tooltip("Enable parallax in the Y direction.")]
    public bool parallaxY = true;

    [Tooltip("Determines how smooth the parallax movement should be when moving from point A to B. " +
             "If the smoothing factor is high, you take larger steps and reach point B faster, " +
             "but the movement can look abrupt. If the smoothing factor is low, you take smaller steps, " +
             "reaching point B more slowly, but the movement appears smoother.")]
    public float smoothing = 0.5f;

    private Camera cam;

    private Vector2 startPosition;

    private float parallaxFactor; // Factor by which the parallax effect is scaled
    private float clippingPlane; // The clipping plane distance used to adjust the parallax effect

    private void Start()
    {
        cam = Camera.main;
        startPosition = transform.position;
        if (subject == null)
        {
            subject = cam.transform;
        }
    }

    /// <summary>
    /// Calculate the parallax factor based on camera settings and the subject's distance.
    /// This method is used to adjust the parallax factor dynamically if the camera or subject moves.
    /// </summary>
    private void CalculateParallaxFactor()
    {
        float distanceFromSubject = transform.position.z - subject.position.z;
        clippingPlane = cam.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane);
        parallaxFactor = Mathf.Abs(distanceFromSubject) / clippingPlane;
    }

    private void Update()
    {
        CalculateParallaxFactor();

        // Calculate the new position based on the original position, camera movement, and parallax factor
        Vector2 travel = (Vector2)cam.transform.position - startPosition;
        Vector2 pos = startPosition + travel * parallaxFactor;
        Vector3 targetPosition = new Vector3( parallaxX ? pos.x : startPosition.x, parallaxY ? pos.y : startPosition.y, transform.position.z);

        if (useSmoothing && smoothing != 0)
        {
            // Smoothly transition to the target position using linear interpolation.
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
        else
        {
            // transition to the target position immediately.
            transform.position = targetPosition;
        }
    }
}
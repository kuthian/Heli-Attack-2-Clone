using UnityEngine;

/// <summary>
/// Component for triggering Wwise audio events associated with the start and destruction of a GameObject.
/// </summary>
public class GameObjectSounds : MonoBehaviour
{
    /// <summary>
    /// Wwise Event triggered when the GameObject starts.
    /// </summary>
    [SerializeField]
    private AK.Wwise.Event wwOnStart;

    /// <summary>
    /// Wwise Event triggered when the GameObject is destroyed.
    /// </summary>
    [SerializeField]
    private AK.Wwise.Event wwOnDestroy;

    /// <summary>
    /// Called when the script is enabled. Triggers the Wwise Event associated with the GameObject's start.
    /// </summary>
    private void Start()
    {
        if (wwOnStart.IsValid())
        {
            wwOnStart.Post(gameObject);
        }
    }

    /// <summary>
    /// Called when the GameObject is being destroyed. Triggers the Wwise Event associated with the GameObject's destruction.
    /// </summary>
    private void OnDestroy()
    {
        if (wwOnDestroy.IsValid())
        {
            wwOnDestroy.Post(gameObject);
        }
    }
}
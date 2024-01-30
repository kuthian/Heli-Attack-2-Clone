using UnityEngine;
using static UnityEditor.FilePathAttribute;

/// <summary>
/// Component for triggering Particle Systems associated with the start and destruction of a GameObject.
/// </summary>
public class GameObjectParticles: MonoBehaviour
{
    /// <summary>
    /// Particle System triggered when the GameObject starts.
    /// </summary>
    [SerializeField]
    private ParticleSystem onStart;

    /// <summary>
    /// Set to "Local" if you want the particle source to move with the GameObject.
    /// Set to "World" if you want the particle source to stay immobile.
    /// </summary>
    [SerializeField]
    private ParticleSystemSimulationSpace onStartSimulationSpace = ParticleSystemSimulationSpace.World;

    /// <summary>
    /// Destroy the Particle System after this many seconds. Set to 0 to ignore this setting.
    /// </summary>
    [SerializeField]
    private float onStartLifetimeSeconds = 0;

    /// <summary>
    /// Particle Systemtriggered when the GameObject is destroyed.
    /// </summary>
    [SerializeField]
    private ParticleSystem onDestroy;

    /// <summary>
    /// Destroy the Particle System after this many seconds. Set to 0 to ignore this setting.
    /// </summary>
    [SerializeField]
    private float onDestroyLifetimeSeconds = 0;

    /// <summary>
    /// Called when the script is enabled. Triggers the Particle System associated with the GameObject's start.
    /// </summary>
    private void Start()
    {
        if (onStart)
        {
            var main = onStart.main;
            main.simulationSpace = onStartSimulationSpace;
            ParticleSystem particle = Instantiate(onStart, transform.position, transform.rotation, transform);
            if (onStartLifetimeSeconds > 0)
            {
                Destroy(particle.gameObject, onStartLifetimeSeconds);
            }
        }
    }

    /// <summary>
    /// Called when the GameObject is being destroyed. Triggers the Particle System associated with the GameObject's destruction.
    /// </summary>
    private void OnDestroy()
    {
        if (onDestroy)
        {
            ParticleSystem particle = Instantiate(onDestroy, transform.position, Quaternion.identity);
            if (onDestroyLifetimeSeconds > 0)
            {
                Destroy(particle.gameObject, onDestroyLifetimeSeconds);
            }
        }
    }
}
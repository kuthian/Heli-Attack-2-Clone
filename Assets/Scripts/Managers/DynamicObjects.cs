using UnityEngine;

/// <summary>
/// Singleton class for managing different types of dynamic objects in the game.
/// </summary>
public class DynamicObjects : SceneSingleton<DynamicObjects>
{
    /// <summary>
    /// Gets the transform of the "Particles" game object.
    /// </summary>
    public static Transform Particles
    {
        get
        {
            return GameObject.Find("DynamicObjects/Particles").transform;
        }
    }

    /// <summary>
    /// Gets the transform of the "Projectiles" game object.
    /// </summary>
    public static Transform Projectiles
    {
        get
        {
            return GameObject.Find("DynamicObjects/Projectiles").transform;
        }
    }

    /// <summary>
    /// Gets the transform of the "Sounds" game object.
    /// </summary>
    public static Transform Sounds
    {
        get
        {
            return GameObject.Find("DynamicObjects/Sounds").transform;
        }
    }

    /// <summary>
    /// Gets the transform of the "Items" game object.
    /// </summary>
    public static Transform Items
    {
        get
        {
            return GameObject.Find("DynamicObjects/Items").transform;
        }
    }

    /// <summary>
    /// Reparent a child transform under a new parent transform.
    /// </summary>
    /// <param name="child">The child transform to reparent.</param>
    /// <param name="parent">The new parent transform.</param>
    public static void Reparent(Transform child, Transform parent)
    {
        child.parent = parent;
    }

    /// <summary>
    /// Reparent a child game object under a new parent transform.
    /// </summary>
    /// <param name="child">The child game object to reparent.</param>
    /// <param name="parent">The new parent transform.</param>
    public static void Reparent(GameObject child, Transform parent)
    {
        // Delegate to the Transform-based Reparent method.
        Reparent(child.transform, parent);
    }

};
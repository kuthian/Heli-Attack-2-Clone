using UnityEngine;

// Singleton that must be attached to a GameObject in the scene.
// Lifetime does not extend beyond the scene.
public class SceneSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance = null;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    Debug.LogError("An instance of " + typeof(T) +
                       " is needed in the scene, but there is none.");
                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
    }
}
using UnityEngine;

// Self-Creating Persistent Singleton
public class SCPSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    // Lock object for thread safety
    private static readonly object lockObject = new object();

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning($"[Singleton] Instance '{typeof(T)}' already destroyed on application quit. Won't create again - returning null.");
                return null;
            }

            lock (lockObject)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        GameObject singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<T>();
                        singletonObject.name = $"{typeof(T).ToString()} (Singleton)";

                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return instance;
            }
        }
    }

    private static bool applicationIsQuitting = false;

    // Call this method to make sure the instance is destroyed when the application is quitted
    public void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}
using UnityEngine;

// Self-Creating Persistent Singleton
public class PersistentSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning($"[Singleton] Instance '{typeof(T)}' already destroyed on application quit. Won't create again - returning null.");
                return null;
            }

            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<T>();
                    singletonObject.name = $"{typeof(T).ToString()}";

                    DontDestroyOnLoad(singletonObject);
                }
            }

            return instance;
        }
    }

    private static bool applicationIsQuitting = false;

    // Call this method to make sure the instance is destroyed when the application is exited
    public virtual void OnDestroy()
    {
        applicationIsQuitting = true;
    }

    // Utility function to create the singleton
    public static void Init()
    {
        // dummy function call
        Instance.GetType();
    }
}
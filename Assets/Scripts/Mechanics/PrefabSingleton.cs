using UnityEngine;

// Self-creating Singleton created from a prefab in AssetPath with the same name as the singleton.
public class PrefabSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static string AssetPath = "Singletons/";

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
                var prefab = Resources.Load<T>("Singletons/" + typeof(T).Name);

                if (prefab == null)
                {
                    Debug.LogError("Singleton prefab missing: " + AssetPath + typeof(T).Name);
                } 
                else
                {
                    instance = Instantiate(prefab);
                    instance.name = $"{typeof(T).ToString()}";
                }
            }

            return instance;
        }
    }

    private static bool applicationIsQuitting = false;

    // Call this method to make sure the instance is destroyed when the application is quitted
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
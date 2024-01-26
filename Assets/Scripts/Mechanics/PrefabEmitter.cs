using System.Collections;
using UnityEngine;

public class PrefabEmitter : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    // Time interval in seconds.
    public float spawnInterval = 5.0f;

    // Use this for initialization.
    void Start()
    {
        // Start the SpawnPrefab coroutine.
        StartCoroutine(SpawnPrefab());
    }

    // Coroutine to spawn the prefab at intervals.
    IEnumerator SpawnPrefab()
    {
        // This loop runs forever.
        while (true)
        {
            // Instantiate the prefab.
            Instantiate(prefab, transform.position, Quaternion.identity, transform);

            // Print a message to the console (optional).
            Debug.Log("Prefab emitted!");

            // Wait for the specified interval then continue the loop.
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
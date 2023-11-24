using UnityEngine;

static class CrateGenerator
{
    public static void SpawnCrateRandom(Vector3 position)
    {
        SpawnCrate(Utils.RandomInRange(ItemAssets.i.CratePrefabs), position);
    }

    public static void SpawnCrate(GameObject cratePrefab, Vector3 position)
    {
        GameObject.Instantiate(cratePrefab, position, Quaternion.identity, DynamicObjects.Items);
    }

};
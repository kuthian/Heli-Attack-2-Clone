using UnityEngine;

static class CrateGenerator
{
    public static void SpawnWeaponCrateRandom(Vector3 position)
    {
        SpawnCrate(Utils.RandomInRange(ItemAssets.i.WeaponCratePrefabs), position);
    }

    public static void SpawnHealthCrate(Vector3 position)
    {
        SpawnCrate(ItemAssets.i.HealthCratePrefab, position);
    }

    public static void SpawnCrate(GameObject cratePrefab, Vector3 position)
    {
        GameObject.Instantiate(cratePrefab, position, Quaternion.identity, DynamicObjects.Items);
    }

};
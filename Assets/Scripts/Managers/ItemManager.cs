using UnityEngine;

public class ItemManager : SceneSingleton<ItemManager>
{
    public static GameObject GetRiflePrefab()
    {
        return ItemAssets.i.RiflePrefab;
    }

    public static GameObject GetUziPrefab()
    {
        return ItemAssets.i.UziPrefab;
    }

    public static GameObject GetShotgunPrefab()
    {
        return ItemAssets.i.ShotgunPrefab;
    }
}
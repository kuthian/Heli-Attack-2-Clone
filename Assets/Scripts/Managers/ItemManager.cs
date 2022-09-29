using UnityEngine;

public class ItemManager : Singleton<ItemManager> {

  public static Sprite GetInventorySpriteByType( GunType gunType )
  {
    foreach (var gun in ItemAssets.i.GunItems)
    {
      if ( gun.type == gunType )
        return gun.crateSprite;
    }
    return null;
  }

  public static Sprite GetInventorySpriteByTag( string tag )
  {
    return GetInventorySpriteByType( ItemTypes.GunTypeFromString(tag) );
  }

  public static Sprite GetInventorySprite( ConsumableType consumableType )
  {
    return null;
  }

  public static void SpawnItemCrateRandom( Vector3 position )
  {
    SpawnItemCrate( Utils.RandomInRange(ItemAssets.i.GunItems), position );
  }

  public static void SpawnItemCrate( GunItem gunItem, Vector3 position )
  {
    GameObject cratePrefab = Resources.Load<GameObject>("__Crate");
    GameObject obj = Instantiate( cratePrefab, position, Quaternion.identity );
    gunItem.gunPrefab.tag = ItemTypes.ToString( gunItem.type );
    obj.GetComponent<SpriteRenderer>().sprite = gunItem.crateSprite;
    obj.AddComponent<WeaponCrate>().weapon = gunItem.gunPrefab;
    DynamicObjects.Reparent( obj, DynamicObjects.Items );
  }

}
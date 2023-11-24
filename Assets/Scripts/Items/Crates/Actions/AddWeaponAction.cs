using UnityEngine;

class AddWeaponAction : __CrateAction
{
    public GameObject gunPrefab;

    override public void Do(GameObject obj)
    {
        obj.BroadcastMessage("AddWeapon", gunPrefab);
    }
}
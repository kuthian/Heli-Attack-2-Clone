using UnityEngine;

class HealAction : __CrateAction {

  [SerializeField] private int healBy = 20;

  override public void Do( GameObject obj )
  {
    obj.SendMessage("Heal", healBy);
  }
}
using UnityEngine;

public class HUDManager : Singleton<HUDManager> {

  [SerializeField]
  private HealthBar healthBar;

  [SerializeField]
  private ReloadBar reloadBar;

  [SerializeField]
  private InventoryHUD inventoryHUD;

  [SerializeField]
  private ScoreCount scoreCount;

  public static HealthBar HealthBar {
    get {
      return Instance.healthBar;
    }
  }

  public static ReloadBar ReloadBar {
    get {
      return Instance.reloadBar;
    }
  }

  public static InventoryHUD Inventory {
    get {
      return Instance.inventoryHUD;
    }
  }

  public static ScoreCount ScoreCount {
    get {
      return Instance.scoreCount;
    }
  }

};
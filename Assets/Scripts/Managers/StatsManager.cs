using UnityEngine;

public class StatsManager : SCPSingleton<StatsManager> {

  public int BulletsFired { get; private set; }
  public int BulletsHit { get; private set; }

  void Start()
  {
    Reset();
  }

  void Reset()
  {
    BulletsFired = 0;
    BulletsHit = 0;
  }

  public static void RegisterBulletFired()
  {
    Instance.BulletsFired++;
  }

  public static void RegisterBulletsFired( int bulletsFired )
  {
    Instance.BulletsFired += bulletsFired; 
  }

  public static void RegisterBulletHit()
  {
    Instance.BulletsHit++;
  }

  public static double AccuracyPercentage()
  {
    if (Instance.BulletsFired == 0) return 0;
    return 100 * (double) Instance.BulletsHit / Instance.BulletsFired;
  }

};
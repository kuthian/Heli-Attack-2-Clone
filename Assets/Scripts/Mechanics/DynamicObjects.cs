using UnityEngine;

public class DynamicObjects : Singleton<DynamicObjects> {

  public static Transform Particles {
    get {
      return GameObject.Find("DynamicObjects/Particles").transform;
    }
  }

  public static Transform Projectiles {
    get {
      return GameObject.Find("DynamicObjects/Projectiles").transform;
    }
  }

  public static Transform Sounds {
    get {
      return GameObject.Find("DynamicObjects/Sounds").transform;
    }
  }

  public static Transform Items {
    get {
      return GameObject.Find("DynamicObjects/Items").transform;
    }
  }

  public static void Reparent( Transform child, Transform parent )
  { 
    child.parent = parent;
  }

  public static void Reparent( GameObject child, Transform parent )
  {
    Reparent( child.transform, parent );
  }

};
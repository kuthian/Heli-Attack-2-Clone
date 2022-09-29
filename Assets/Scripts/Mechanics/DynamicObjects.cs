using UnityEngine;

public class DynamicObjects : Singleton<DynamicObjects> {
  
  new public static Transform transform {
    get {
      return GameObject.Find("DynamicObjects").transform;
    }
  }

  public static void AssignChild( GameObject obj )
  { 
    obj.transform.parent = transform;
  }

};
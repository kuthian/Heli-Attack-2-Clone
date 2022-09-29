using UnityEngine;
using System.Collections;
using System.Collections.Generic;

static class Utils
{ 

  public static float GetAngleFromVectorFloat(Vector3 dir)
  {
    dir = dir.normalized;
    float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    if (n < 0) n += 360;
    return n;
  }

  public static T RandomInRange<T>( T[] array )
  {
    return array[Random.Range(0, array.Length)];
  }

};
using System;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public GameObject bar;

    public void SetPercentFill(float percent)
    {
        bar.transform.localScale = new Vector3(bar.transform.localScale.x, (percent / 100f), bar.transform.localScale.z);
    }

}
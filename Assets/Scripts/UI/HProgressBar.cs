using System;
using UnityEngine;

public class HProgressBar : ProgressBar
{
    override protected void SetPercentFill(float value)
    {
        Vector2 sizeDelta = mask.sizeDelta;
        sizeDelta.x = (value / 100) * fill.rect.width;
        mask.sizeDelta = sizeDelta;
    }

}
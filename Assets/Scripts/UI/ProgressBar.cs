using System;
using UnityEngine;

public class VProgressBar : ProgressBar
{
    override protected void SetPercentFill(float value)
    {
        Vector2 sizeDelta = mask.sizeDelta;
        sizeDelta.y = (value / 100) * fill.rect.height;
        mask.sizeDelta = sizeDelta;
    }

}
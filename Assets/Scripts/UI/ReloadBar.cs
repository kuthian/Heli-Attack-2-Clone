using UnityEngine;
using UnityEngine.UI;

public class ReloadBar : MonoBehaviour
{
    internal Slider slider;

    public void SetCooldownTime(float cooldownTime)
    {
        slider = GetComponent<Slider>();
        slider.maxValue = cooldownTime;
        slider.value = cooldownTime;
    }

    public void SetTimeRemaining(float timeRemaining)
    {
        slider.value = slider.maxValue - timeRemaining;
    }
}
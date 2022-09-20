using UnityEngine;
using UnityEngine.UI;

public class ReloadBar : MonoBehaviour {

  [SerializeField] private Slider slider;

  public void SetCooldownTime(float cooldownTime)
  {
    slider.maxValue = cooldownTime;
    slider.value = cooldownTime;
  }

  public void SetTimeRemaining(float timeRemaining)
  {
    slider.value = slider.maxValue - timeRemaining;
  }
}
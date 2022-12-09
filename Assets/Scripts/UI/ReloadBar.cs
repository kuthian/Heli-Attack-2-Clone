using UnityEngine;
using UnityEngine.UI;

public class ReloadBar : MonoBehaviour {

  internal Slider _slider;

  public void SetCooldownTime(float cooldownTime)
  {
    _slider = GetComponent<Slider>();
    _slider.maxValue = cooldownTime;
    _slider.value = cooldownTime;
  }

  public void SetTimeRemaining(float timeRemaining)
  {
    _slider.value = _slider.maxValue - timeRemaining;
  }
}
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

  [SerializeField] private Slider slider;
  [SerializeField] private Health health;

  public void Start()
  {
    slider.maxValue = health.MaxHealth;
    slider.value = health.CurrentHealth;
    health.OnHealthChanged += HandleOnHealthChanged;
  }

  private void HandleOnHealthChanged(int health)
  {
    slider.value = health;
  }

}
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

  [SerializeField] private Health _health;
  internal Slider _slider;

  public void Start()
  {
    _slider = GetComponent<Slider>();
    _slider.maxValue = _health.MaxHealth;
    _slider.value = _health.CurrentHealth;
    _health.OnHealthChanged += HandleOnHealthChanged;
  }

  private void OnEnable()
  {
    _health.OnHealthChanged += HandleOnHealthChanged;
  }

  private void OnDisable()
  {
    _health.OnHealthChanged -= HandleOnHealthChanged;
  }

  private void HandleOnHealthChanged(int health)
  {
    _slider.value = health;
  }

}
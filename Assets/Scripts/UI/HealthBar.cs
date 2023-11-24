using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    internal Slider slider;

    public void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = _health.MaxHealth;
        slider.value = _health.CurrentHealth;
        _health.OnHealthChanged += HandleOnHealthChanged;
        AkSoundEngine.SetRTPCValue("health", slider.value);
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
        slider.value = health;
        AkSoundEngine.SetRTPCValue("health", slider.value);
    }

}
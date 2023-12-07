using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    internal Slider slider;

    public void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = health.MaxHealth;
        slider.value = health.CurrentHealth;
        health.OnHealthChanged += HandleOnHealthChanged;
        AkSoundEngine.SetRTPCValue("health", slider.value);
    }

    private void OnEnable()
    {
        health.OnHealthChanged += HandleOnHealthChanged;
    }

    private void OnDisable()
    {
        health.OnHealthChanged -= HandleOnHealthChanged;
    }

    private void HandleOnHealthChanged(int health)
    {
        slider.value = health;
        AkSoundEngine.SetRTPCValue("health", slider.value);
    }

}
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    internal ProgressBar bar;

    public void Start()
    {
        bar = GetComponent<ProgressBar>();
        bar.Percentage = health.CurrentHealth;
        health.OnHealthChanged += HandleOnHealthChanged;
        AkSoundEngine.SetRTPCValue("health", bar.Percentage);
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
        bar.Percentage = health;
        AkSoundEngine.SetRTPCValue("health", bar.Percentage);
    }

}
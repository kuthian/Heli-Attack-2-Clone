using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ReloadBar : MonoBehaviour
{
    internal ProgressBar progressBar;
    private float cooldownTime;

    private void Awake()
    {
        progressBar = GetComponentInChildren<ProgressBar>();
    }

    public void SetCooldownTime(float time)
    {
        cooldownTime = time;
    }

    public void SetTimeRemaining(float timeRemaining)
    {
        progressBar.Percentage = 100 * (1 - (timeRemaining / cooldownTime));
    }
}
using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraShaker : SceneSingleton<CameraShaker>
{
    internal CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;
    private float shakeTimer = 0f;
    private float shakeTimerTotal = 0f;
    private float startingIntensity;

    override protected void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        if (virtualCamera != null)
        {
            virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }
    }

    private void Update()
    {
        // Gradually reduce shake intensity based on the remaining time
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            virtualCameraNoise.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
        }
    }

    public static void ShakeCamera(float intensity, float time)
    {
        Instance._ShakeCamera(intensity, time);
    }

    private void _ShakeCamera(float intensity, float time)
    {
        if (virtualCameraNoise != null)
        {
            if (time > shakeTimer)
            {
                shakeTimer = time;
                shakeTimerTotal = time;
                startingIntensity = intensity;
            }
            virtualCameraNoise.m_AmplitudeGain = intensity;
        }
    }

};
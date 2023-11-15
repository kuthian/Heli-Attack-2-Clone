using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSounds : MonoBehaviour
{
    public AK.Wwise.Event lightFlicker;


    public void wwLightFlicker()
    {
        lightFlicker.Post(gameObject);
    }
}

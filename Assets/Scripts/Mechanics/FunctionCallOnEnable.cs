using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class FunctionCallOnEnable : MonoBehaviour
{
    public UnityEvent functionCall;

    public void OnEnable()
    {
        functionCall.Invoke();
    }
}
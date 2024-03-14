using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class FunctionCallOnDisable: MonoBehaviour
{
    public UnityEvent functionCall;

    public void OnDisable()
    {
        functionCall.Invoke();
    }
}
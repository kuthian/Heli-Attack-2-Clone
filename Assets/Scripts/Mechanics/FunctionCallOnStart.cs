using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class FunctionCallOnStart: MonoBehaviour
{
    public UnityEvent functionCall;

    public void Start()
    {
        functionCall.Invoke();
    }
}
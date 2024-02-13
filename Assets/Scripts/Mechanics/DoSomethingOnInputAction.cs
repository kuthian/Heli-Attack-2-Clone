using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DoSomethingOnInputAction : MonoBehaviour
{
    public UnityEvent functionCall;
    public InputActionReference anyKeyAction;

    private void OnEnable()
    {
        anyKeyAction.action.performed += OnAnyKey;
        anyKeyAction.action.Enable();
    }

    private void OnDisable()
    {
        anyKeyAction.action.performed -= OnAnyKey;
        anyKeyAction.action.Disable();
    }

    private void OnAnyKey(InputAction.CallbackContext context)
    {
        functionCall.Invoke();
    }
}

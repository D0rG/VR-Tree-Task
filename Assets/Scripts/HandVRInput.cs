using UnityEngine;
using UnityEngine.InputSystem;

public class HandVRInput: MonoBehaviour, IControllerInfo
{
    [SerializeField] private InputActionReference _grip;
    [SerializeField] private InputActionReference _trigger;
    public Transform controller;
    public bool gripState { get; private set; }
    public bool triggerState { get; private set; }

    private void Awake()
    {
        gripState = false;
        triggerState = false;

        _grip.action.started += GripActivated;
        _grip.action.canceled += GripDeactivated;

        _trigger.action.started += TriggerActivated;
        _trigger.action.canceled += TriggerDeactivated;
    }

    private void OnDestroy()
    {
        _grip.action.started -= GripActivated;
        _grip.action.canceled -= GripDeactivated;

        _trigger.action.started -= TriggerActivated;
        _trigger.action.canceled -= TriggerDeactivated;
    }

    private void GripActivated(InputAction.CallbackContext context)
    {
        Debug.Log($"Grip Activated");
        gripState = true;
    }

    private void GripDeactivated(InputAction.CallbackContext context)
    {
        Debug.Log($"Grip Deactivated");
        gripState = false;
    }

    private void TriggerActivated(InputAction.CallbackContext context)
    {
        Debug.Log($"Trip Activated");
        triggerState = true;
    }

    private void TriggerDeactivated(InputAction.CallbackContext context)
    {
        Debug.Log($"Trip Deactivated");
        triggerState = false;
    }
}

public interface IControllerInfo
{
    public bool gripState { get; }
    public bool triggerState { get; }
}

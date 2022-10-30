using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputVRInteraction : MonoBehaviour
{
    [SerializeField] private InputActionReference _gripLeft;
    [SerializeField] private InputActionReference _gripRight;

    [SerializeField] private MeshRenderer _renderer;

    private void Awake()
    {
        _gripLeft.action.started += GripActivated;
    }

    private void OnDestroy()
    {
        _gripLeft.action.started -= GripActivated;
    }

    private void GripActivated(InputAction.CallbackContext context)
    {
        Debug.Log($"Grip Left {context.action}");
        _renderer.enabled = !_renderer.enabled;
    }
}

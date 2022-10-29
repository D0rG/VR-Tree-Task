using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRotator : MonoBehaviour
{
    [SerializeField] private GameObject _textCanvas;
    [SerializeField] private GameObject _lookAtObject;

    private void Awake()
    {
        _lookAtObject = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void FixedUpdate()
    {
        _textCanvas.transform.LookAt(_lookAtObject.transform);
    }
}

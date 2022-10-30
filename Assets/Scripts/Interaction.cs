using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Transform _treeObject;
    [SerializeField] private HandVRInput _leftControllerInfo;
    [SerializeField] private HandVRInput _rightControllerInfo;
    private bool _isFirstDoublegrip = true;
    private bool _leftGripBefore = false;
    private bool _rightGripBefore = false;
    private Vector3 _size = Vector3.one;
    private float controllerDistanceMax = 0;

    private void Update()
    {
        var controllerDistance = Vector3.Distance(_leftControllerInfo.controller.position, _rightControllerInfo.controller.position);

        if (_isFirstDoublegrip)
        {
            _size = Vector3.one;
        }

        if (_leftControllerInfo.gripState && _rightControllerInfo.gripState)
        {
            if (_isFirstDoublegrip)
            {
                Debug.Log("Оба контроллера сжаты в первый раз");
                controllerDistanceMax = controllerDistance;
                _size = _treeObject.transform.localScale;
            }
            else
            {
                var procent = controllerDistance / controllerDistanceMax;
                _treeObject.transform.localScale = _size * procent;
            }
        }


        _leftGripBefore = _leftControllerInfo.gripState;
        _rightGripBefore = _rightControllerInfo.gripState;
        _isFirstDoublegrip = !(_leftControllerInfo.gripState && _rightControllerInfo.gripState);
    }
}

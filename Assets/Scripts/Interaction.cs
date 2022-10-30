using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Transform _treeObject;
    [SerializeField] private HandVRInput _leftControllerInfo;
    [SerializeField] private HandVRInput _rightControllerInfo;
    private bool _isFirstDoublegrip = true;
    private bool _leftGripBefore = false;
    private bool _rightGripBefore = false;
    private Vector3 _size = Vector3.one;
    private float _controllerDistanceMax = 0;
    private Transform _sphereTransform;
    private Transform _pointToRotate;

    private void Awake()
    {
        var sphere = GameObject.CreatePrimitive(PrimitiveType.Cube);
        sphere.GetComponent<MeshRenderer>().enabled = false;
        sphere.GetComponent<BoxCollider>().enabled = false;
        _sphereTransform = sphere.GetComponent<Transform>();
        _sphereTransform.localScale = Vector3.one * 0.2f;
        _sphereTransform.parent = _leftControllerInfo.transform;
        _sphereTransform.position = _leftControllerInfo.transform.position;
    }

    private void Update()
    {
        var controllerDistance = Vector3.Distance(_leftControllerInfo.controller.position, _rightControllerInfo.controller.position);

        if (_isFirstDoublegrip)
        {
            _size = Vector3.one;
        }

        if (_leftControllerInfo.gripState && _rightControllerInfo.gripState)
        {
            Look();
            ConnectTree();
            if (_isFirstDoublegrip)
            {
                _controllerDistanceMax = controllerDistance;
                _size = _treeObject.transform.localScale;
            }
            else
            {
                var procent = controllerDistance / _controllerDistanceMax;
                _treeObject.transform.localScale = _size * procent;
            }
        }
        else if(_leftControllerInfo.gripState)
        {
            _leftGripBefore = true;
            SetupConnection(_leftControllerInfo, _rightControllerInfo.transform);
        }
        else if(_rightControllerInfo.gripState)
        {
            _rightGripBefore = true;
            SetupConnection(_rightControllerInfo, _leftControllerInfo.transform);
        }
        else
        {
            DisconnectTree();
            _leftGripBefore = false;
            _rightGripBefore = false;
        }

        _leftGripBefore = _leftControllerInfo.gripState;
        _rightGripBefore = _rightControllerInfo.gripState;
        _isFirstDoublegrip = !(_leftControllerInfo.gripState && _rightControllerInfo.gripState);
    }

    private void Look()
    {
        _sphereTransform.LookAt(_pointToRotate);
    }

    private void SetupConnection(HandVRInput controller, Transform toRotatePoint)
    {
        DisconnectTree();
        _sphereTransform.parent = controller.transform;
        _sphereTransform.position = controller.transform.position;
        _sphereTransform.rotation = Quaternion.identity;
        _pointToRotate = toRotatePoint;
    }

    private void ConnectTree()
    {
        _treeObject.transform.parent = _sphereTransform;
    }

    private void DisconnectTree()
    {
        _treeObject.transform.parent = null;
    } 
}

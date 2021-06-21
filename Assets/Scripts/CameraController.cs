using System;
using System.Collections;
using System.Collections.Generic;
using Race;
using UnityEditorInternal;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Bike _targetBike;
    [SerializeField] private float _minFov = 60;
    [SerializeField] private float _maxFov = 85;

    [SerializeField] private float _shakeFactor;
    [SerializeField] private AnimationCurve _shakeCurve;

    private Vector3 _initLocalPosition;

    private void Start()
    {
        _initLocalPosition = Camera.main.transform.localPosition;
    }

    private void Update()
    {
        UpdateFov();
        UpdateCameraShake();
    }

    private void UpdateCameraShake()
    {
        var cam = Camera.main;
        var t = _targetBike.GetNormalizedSpeed();
        var curveValue = _shakeCurve.Evaluate(t);

        var ramdomVector = UnityEngine.Random.insideUnitSphere * _shakeFactor;
        ramdomVector.z = 0;

        cam.transform.localPosition = _initLocalPosition + ramdomVector * curveValue;
    }

    private void UpdateFov()
    {
        var cam = Camera.main;

        var t = _targetBike.GetNormalizedSpeed();
        cam.fieldOfView = Mathf.Lerp(_minFov, _maxFov, t);
    }
}
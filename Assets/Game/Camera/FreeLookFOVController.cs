using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class FreeLookFOVController : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook cinemachineFreeLook;
    [SerializeField] private float defaultFOV = 40;
    [SerializeField] private float minFov, maxFov;
    [SerializeField] private float zoomSpeed = 1;
    [SerializeField] private float zoomLerpSpeed = 1;

    private float Fov
    {
        get => cinemachineFreeLook.m_Lens.FieldOfView;
        set => cinemachineFreeLook.m_Lens.FieldOfView = Mathf.Clamp(value, minFov, maxFov);
    }

    private float _fovTarget;
    
    private float FovTarget
    {
        get => _fovTarget;
        set => _fovTarget = Mathf.Clamp(value, minFov, maxFov);
    }

    private void Start()
    {
        Fov = defaultFOV;
        FovTarget = Fov;
    }

    public void HandleScroll(float inputValue)
    {
        FovTarget -= inputValue * zoomSpeed * Time.deltaTime;
    }

    private void Update()
    {
        Fov = Mathf.Lerp(Fov, FovTarget, zoomLerpSpeed * Time.deltaTime);
    }
}

using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownCamState : State
{
    private CinemachineInputProvider _inputProvider;
    private InputActionReference _emptyInput;
    private CinemachineFreeLook _freeLook;
    private float _resetTransitionSpeed;

    public TopDownCamState(CinemachineInputProvider inputProvider, InputActionReference emptyInput, CinemachineFreeLook freeLook, float resetTransitionSpeed)
    {
        _inputProvider = inputProvider;
        _emptyInput = emptyInput;
        _freeLook = freeLook;
        _resetTransitionSpeed = resetTransitionSpeed;
    }

    public override void Enter()
    {
        _inputProvider.XYAxis = _emptyInput;
        _freeLook.GetRig(0).DestroyCinemachineComponent<CinemachineHardLookAt>();
    }

    public override void Tick()
    { 
        _freeLook.m_YAxis.Value = Mathf.MoveTowards(_freeLook.m_YAxis.Value, 1, _resetTransitionSpeed * Time.deltaTime);
    }
}
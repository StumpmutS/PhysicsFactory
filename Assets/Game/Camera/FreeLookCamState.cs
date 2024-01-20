using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class FreeLookCamState : State
{
    private CinemachineInputProvider _inputProvider;
    private InputActionReference _defaultInput;

    public FreeLookCamState(CinemachineInputProvider inputProvider, InputActionReference defaultInput)
    {
        _inputProvider = inputProvider;
        _defaultInput = defaultInput;
    }

    public override void Enter()
    {
        _inputProvider.XYAxis = _defaultInput;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
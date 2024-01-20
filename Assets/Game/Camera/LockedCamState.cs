using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class LockedCamState : State
{
    private CinemachineInputProvider _inputProvider;
    private InputActionReference _emptyInput;

    public LockedCamState(CinemachineInputProvider inputProvider, InputActionReference emptyInput)
    {
        _inputProvider = inputProvider;
        _emptyInput = emptyInput;
    }

    public override void Enter()
    {
        _inputProvider.XYAxis = _emptyInput;
        Cursor.lockState = CursorLockMode.None;
    }
}
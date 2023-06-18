using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class FreeLookController : MonoBehaviour
{
    [SerializeField] private CinemachineInputProvider inputProvider;
    [FormerlySerializedAs("defaultInput")] [SerializeField] private InputActionReference lookInput;
    [FormerlySerializedAs("emptyInput")] [SerializeField] private InputActionReference emptyVector2Input;
    [SerializeField] private CinemachineFreeLook freeLook;
    [SerializeField] private float resetTransitionSpeed;

    private StateMachine _stateMachine;
    private bool _freeLookActive;
    
    private void Awake()
    {
        var topDownCam = new RTSCamState(inputProvider, emptyVector2Input, freeLook, resetTransitionSpeed);
        var freeLookCam = new FreeLookCamState(inputProvider, lookInput);
        _stateMachine = new StateMachine(topDownCam);
        _stateMachine.AddTransition(topDownCam, freeLookCam, () => _freeLookActive);
        _stateMachine.AddTransition(freeLookCam, topDownCam, () => !_freeLookActive);
    }

    private void Start()
    {
        _stateMachine.Init();
    }

    private void Update()
    {
        _stateMachine.Tick();
    }

    public void Inspect(bool value)
    {
        _freeLookActive = value;
    }
}
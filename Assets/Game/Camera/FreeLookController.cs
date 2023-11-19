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
    private bool _inspecting;
    private bool _home;
    
    private void Awake()
    {
        var topDownCam = new TopDownCamState(inputProvider, emptyVector2Input, freeLook, resetTransitionSpeed);
        var freeLookCam = new FreeLookCamState(inputProvider, lookInput);
        var lockedCam = new LockedCamState(inputProvider, emptyVector2Input);
        _stateMachine = new StateMachine(topDownCam);
        _stateMachine.AddAnyTransition(topDownCam, () => _home);
        _stateMachine.AddAnyTransition(freeLookCam, () => _inspecting);
        _stateMachine.AddTransition(freeLookCam, lockedCam, () => !_inspecting && !_home);
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
        _inspecting = value;
        _home = false;
    }

    public void Home()
    {
        _home = true;
        _inspecting = false;
    }
}
using System;
using FMPUtils.Extensions;
using UnityEngine;
using UnityEngine.Events;

public class SpringController : MonoBehaviour
{
    [SerializeField] private DeltaTimeReference timeReference;
    [SerializeField, Range(-1, 1)] private float startValue = 0;
    [SerializeField] private float frequency;
    [SerializeField] private float damping;

    private bool _initialized;
    private float _targetValue;
    private float TargetValue
    {
        get
        {
            if (!_initialized) Init();
            return _targetValue;
        }
        set
        {
            if (!_initialized) Init();
            _targetValue = value;
        }
    }
    private float _currentValue;
    private float _currentVelocity;
    private bool _pressed;

    public UnityEvent<float, float> OnSpringValueChanged = new();

    private void Awake()
    {
        foreach (var listener in GetComponents<SpringListener>())
        {
            OnSpringValueChanged.AddListener(listener.TryHandleSpringValue);
        }
        
        if (!_initialized) Init();
    }

    private void Init()
    {
        _targetValue = startValue;
        _currentValue = startValue;
        _initialized = true;
    }

    private void Start()
    {
        OnSpringValueChanged.Invoke(_currentValue, TargetValue);
    }

    public void Update()
    {
        SpringMotion.CalcDampedSimpleHarmonicMotion(ref _currentValue, ref _currentVelocity, 
            _targetValue, timeReference.DeltaTime, frequency, damping);
        OnSpringValueChanged.Invoke(_currentValue, _targetValue);
    }

    public virtual void Nudge(float amount)
    {
        _currentVelocity += amount;
    }

    public void SetTarget(float value)
    {
        TargetValue = Mathf.Clamp(value, -1, 1);
    }

    public void SetTargetAndValue(float value)
    {
        SetTarget(value);
        _currentValue = value;
        OnSpringValueChanged.Invoke(_currentValue, TargetValue);
    }
}
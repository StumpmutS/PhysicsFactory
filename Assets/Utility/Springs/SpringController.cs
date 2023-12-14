using System;
using FMPUtils.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class SpringController : MonoBehaviour
{
    [SerializeField] private DeltaTimeReference timeReference;
    [SerializeField, Range(-1, 1)] private float startValue = 0;
    [SerializeField] private float frequency;
    [SerializeField] private float damping;

    private bool _targetSet;
    private float _targetValue;
    private float _currentValue;
    private float _currentVelocity;
    private bool _pressed;

    public UnityEvent<float, float> OnSpringValueChanged;

    private void Awake()
    {
        if (!_targetSet) Init();
    }

    private void Init()
    {
        _targetValue = startValue;
        _currentValue = startValue;
    }

    private void Start()
    {
        OnSpringValueChanged.Invoke(_currentValue, _targetValue);
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
        if (!_targetSet)
        {
            Init();
            _targetSet = true;
        }
        _targetValue = Mathf.Clamp(value, -1, 1);
    }
}
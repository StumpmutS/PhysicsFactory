using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utility.Scripts;

public class SignedFloatSelector : MonoBehaviour
{
    private SignedFloat _signedFloat;
    public SignedFloat SignedFloat
    {
        get => _signedFloat;
        set
        {
            if (value.Equals(_signedFloat)) return;
            _signedFloat = value;
            HandleChange();
        }
    }
    private object _callbackObj;
    private float _maxValue;
    
    public UnityEvent<object, SignedFloat> OnChanged;

    public void Init(SignedFloat value, object callbackObj, float maxValue = float.MaxValue)
    {
        _callbackObj = callbackObj;
        SignedFloat = value;
        _maxValue = maxValue;
    }

    public void AddValue(float value)
    {
        SignedFloat = new SignedFloat(Mathf.Clamp(SignedFloat.Value + value, 0, _maxValue), SignedFloat.Positive);
    }

    public void HandleSignChange(bool value)
    {
        SignedFloat = new SignedFloat(SignedFloat.Value, value);
    }

    private void HandleChange()
    {
        OnChanged.Invoke(_callbackObj, SignedFloat);
    }
}
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utility.Scripts;

public class SignedFloatSelector : MonoBehaviour
{
    [FormerlySerializedAs("text")] [SerializeField] private TMP_Text floatText;
    [SerializeField] private Toggle toggle;
    
    private SignedFloat _signedFloat;
    private SignedFloat SignedFloat
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
    
    public event Action<object, SignedFloat> OnChanged = delegate {  };

    public void Init(SignedFloat value, object callbackObj)
    {
        _callbackObj = callbackObj;
        SignedFloat = value;
        UpdateVisuals(value);
    }

    public void HandleAdd()
    {
        SignedFloat = new SignedFloat(SignedFloat.Value + 1, SignedFloat.Positive);
    }

    public void HandleSubtract()
    {
        if (SignedFloat.Value <= 0) return;
        SignedFloat = new SignedFloat(Mathf.Max(0, SignedFloat.Value - 1), SignedFloat.Positive);
    }

    public void HandleSignChange(bool value)
    {
        SignedFloat = new SignedFloat(SignedFloat.Value, value);
    }

    private void HandleChange()
    {
        OnChanged.Invoke(_callbackObj, SignedFloat);
    }

    public void UpdateVisuals(SignedFloat value)
    {
        _signedFloat = value;
        floatText.text = value.Value.ToString("F2");
        toggle.isOn = value.Positive;
    }
}
﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utility.Scripts;

public class SignedIntegerSelector : MonoBehaviour
{
    private SignedInt _signedInt;
    private SignedInt SignedInt
    {
        get => _signedInt;
        set
        {
            if (value.Equals(_signedInt)) return;
            _signedInt = value;
            HandleChange();
        }
    }
    private object _callbackObj;
    
    public event Action<object, SignedInt> OnChanged = delegate {  };

    public void Init(SignedInt value, object callbackObj)
    {
        _callbackObj = callbackObj;
        SignedInt = value;
    }

    public void HandleAdd()
    {
        SignedInt = new SignedInt(SignedInt.Value + 1, SignedInt.Positive);
    }

    public void HandleSubtract()
    {
        if (SignedInt.Value <= 0) return;
        SignedInt = new SignedInt(SignedInt.Value - 1, SignedInt.Positive);
    }

    public void HandleSignChange(bool value)
    {
        SignedInt = new SignedInt(SignedInt.Value, value);
    }

    private void HandleChange()
    {
        OnChanged.Invoke(_callbackObj, SignedInt);
    }
}
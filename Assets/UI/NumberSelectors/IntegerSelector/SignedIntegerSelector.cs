using UnityEngine;
using UnityEngine.Events;
using Utility.Scripts;

public class SignedIntegerSelector : MonoBehaviour
{
    private SignedInt _signedInt;
    public SignedInt SignedInt
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
    private int _maxValue;

    public UnityEvent<object, SignedInt> OnChanged = new();

    public void Init(SignedInt value, object callbackObj, int maxValue = int.MaxValue)
    {
        _callbackObj = callbackObj;
        SignedInt = value;
        _maxValue = maxValue;
    }

    public void AddValue(int value)
    {
        SignedInt = new SignedInt(Mathf.Clamp(SignedInt.Value + value, 0, _maxValue), SignedInt.Positive);
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

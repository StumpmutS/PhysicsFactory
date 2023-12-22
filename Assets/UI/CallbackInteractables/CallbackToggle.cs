using System;
using UnityEngine;
using UnityEngine.UI;

public class CallbackToggle : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    public Toggle Toggle => toggle;

    private Action<object, bool> _callback;
    private object _callbackObject;

    private void Awake()
    {
        toggle.onValueChanged.AddListener(HandleToggleChanged);
    }

    public void Init(CallbackToggleData data)
    {
        toggle.isOn = data.Value;
        _callback = data.Callback;
        _callbackObject = data.CallbackObject;
    }

    private void HandleToggleChanged(bool value)
    {
        _callback?.Invoke(_callbackObject, value);
    }

    private void OnDestroy()
    {
        if (toggle != null) toggle.onValueChanged.RemoveListener(HandleToggleChanged);
    }
}

public class CallbackToggleData
{
    public Action<object, bool> Callback { get; }
    public object CallbackObject { get; }
    public bool Value { get; }

    public CallbackToggleData(Action<object, bool> callback, object callbackObject, bool value)
    {
        Callback = callback;
        CallbackObject = callbackObject;
        Value = value;
    }
}
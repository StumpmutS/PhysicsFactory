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

    public void Init(Action<object, bool> callback, object callbackObject, bool currentValue)
    {
        toggle.isOn = currentValue;
        _callback = callback;
        _callbackObject = callbackObject;
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

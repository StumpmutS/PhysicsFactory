using System;
using UnityEngine;
using UnityEngine.UI;

public class CallbackButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private Action<object> _callback;
    private object _callbackObject;
    
    private void Awake()
    {
        button.onClick.AddListener(HandleClicked);
    }

    public void Init(Action<object> callback, object callbackObject)
    {
        _callback = callback;
        _callbackObject = callbackObject;
    }

    private void HandleClicked()
    {
        _callback?.Invoke(_callbackObject);
    }

    private void OnDestroy()
    {
        if (button != null) button.onClick.RemoveListener(HandleClicked);
    }
}
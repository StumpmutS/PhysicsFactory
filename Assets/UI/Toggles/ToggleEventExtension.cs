using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleEventExtension : MonoBehaviour
{
    [SerializeField] private Toggle toggle;

    public UnityEvent OnToggleOn = new();
    public UnityEvent OnToggleOff = new();
    
    private void Awake()
    {
        toggle.onValueChanged.AddListener(HandleToggleChanged);
    }

    private void HandleToggleChanged(bool value)
    {
        if (value) OnToggleOn.Invoke();
        else OnToggleOff.Invoke();
    }
}
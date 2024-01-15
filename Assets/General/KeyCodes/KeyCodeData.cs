using System;
using UnityEngine;

[Serializable]
public class KeyCodeData
{
    [SerializeField] private KeyCode keyCode;
    public KeyCode KeyCode => keyCode;
    [SerializeField] private bool optional;
    public bool Optional => optional;
}
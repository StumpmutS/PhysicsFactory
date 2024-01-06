using System;
using UnityEngine;

[Serializable]
public class ContextData
{
    [SerializeField] private string label;
    public string Label => label;
}
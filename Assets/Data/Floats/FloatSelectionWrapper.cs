using System;
using UnityEngine;

[Serializable]
public class FloatSelectionWrapper
{
    [SerializeField] private bool useSpecificValue;
    [SerializeField, ShowIf(nameof(useSpecificValue), false)] private FloatSO floatSo;
    [SerializeField, ShowIf(nameof(useSpecificValue), true)] private float specificValue;

    public float Value => useSpecificValue ? specificValue : floatSo.Value; 
    
    public FloatSelectionWrapper(float value)
    {
        useSpecificValue = true;
        specificValue = value;
    }
}
using System;
using UnityEngine;

[Serializable]
public class ColorSelectionWrapper
{
    [SerializeField] private bool useSpecificColor;
    [SerializeField, ShowIf(nameof(useSpecificColor), false)] private ColorSO colorSo;
    [SerializeField, ShowIf(nameof(useSpecificColor), true)] private Color specificColor;

    public Color Color => useSpecificColor ? specificColor : colorSo.Colors[0]; 
    
    public ColorSelectionWrapper(Color color)
    {
        useSpecificColor = true;
        specificColor = color;
    }
}
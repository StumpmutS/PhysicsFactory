using TMPro;
using UnityEngine;
using Utility.Scripts;

public class FloatSelectorTextListener : SignedFloatSelectorListener
{
    [SerializeField] private TMP_Text text;
    
    protected override void UpdateValue(SignedFloat value)
    {
        text.text = value.Value.ToString("F2");
    }
}
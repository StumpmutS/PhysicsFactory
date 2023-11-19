using UnityEngine;
using UnityEngine.UI;
using Utility.Scripts;

public class FloatSelectorToggleListener : SignedFloatSelectorListener
{
    [SerializeField] private Toggle toggle;
    
    protected override void UpdateValue(SignedFloat value)
    {
        toggle.isOn = value.Positive;
    }
}
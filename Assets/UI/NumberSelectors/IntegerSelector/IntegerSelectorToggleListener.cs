using UnityEngine;
using UnityEngine.UI;
using Utility.Scripts;

public class IntegerSelectorToggleListener : SignedIntegerSelectorListener
{
    [SerializeField] private Toggle toggle;
    
    protected override void UpdateValue(SignedInt value)
    {
        toggle.isOn = value.Positive;
    }
}
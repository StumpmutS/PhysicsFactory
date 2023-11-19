using TMPro;
using UnityEngine;
using Utility.Scripts;

public class IntegerSelectorTextListener : SignedIntegerSelectorListener
{
    [SerializeField] private TMP_Text text;
    
    protected override void UpdateValue(SignedInt value)
    {
        text.text = value.Value.ToString("F2");
    }
}
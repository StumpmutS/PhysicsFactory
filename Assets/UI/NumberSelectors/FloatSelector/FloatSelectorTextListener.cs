using TMPro;
using UnityEngine;
using Utility.Scripts;

public class FloatSelectorTextListener : SignedFloatSelectorListener
{
    [SerializeField] private TMP_Text text;
    
    protected override void UpdateValue(SignedFloat value)
    {
        if (text == null)
        {
            Debug.LogWarning($"Text has not been set in FloatSelectorTextListener on gameobject: {gameObject.name}");
            return;
        }
        
        text.text = value.Value.ToString("F2");
    }
}
using TMPro;
using UnityEngine;

public class LabeledSignedFloatSelector : SignedFloatSelector
{
    [SerializeField] private TMP_Text label;

    public void SetLabel(string value)
    {
        label.text = value;
    }
}
using TMPro;
using UnityEngine;

public class LabeledCallbackToggle : CallbackToggle
{
    [SerializeField] private TMP_Text text;

    public void SetText(string value)
    {
        text.text = value;
    }
}
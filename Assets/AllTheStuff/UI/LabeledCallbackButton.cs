using TMPro;
using UnityEngine;

public class LabeledCallbackButton : CallbackButton
{
    [SerializeField] private TMP_Text text;

    public void SetText(string value)
    {
        text.text = value;
    }
}
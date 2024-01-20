using TMPro;
using UnityEngine;

public class TMPFontStyleSetter : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private void SetStyle(FontStyles styles, bool value)
    {
        if (value) text.fontStyle |= styles;
        else text.fontStyle &= ~styles;
    }

    public void SetBold(bool value) => SetStyle(FontStyles.Bold, value);
}
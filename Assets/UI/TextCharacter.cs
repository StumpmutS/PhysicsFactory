using TMPro;
using UnityEngine;

public class TextCharacter : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TMP_Text text;
    [SerializeField] private float widthRatio = .5f;
    [SerializeField] private float heightRatio = .75f;

    public void SetFontSize(float size)
    {
        text.fontSize = size;
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size * widthRatio);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size * heightRatio);
    }
}

//width 1/2 of font size, height 3/4 of font size
//width of decimal 1/8 of font size
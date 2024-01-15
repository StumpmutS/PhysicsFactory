using UnityEngine;

public static class ScreenFormatter
{
    public static void FormatRect(RectTransform rectTransform, Vector2 point)
    {
        rectTransform.anchoredPosition = point;
    }
}
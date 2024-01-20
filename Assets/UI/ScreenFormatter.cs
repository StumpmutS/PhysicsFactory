using UnityEngine;

public static class ScreenFormatter
{
    public static void PositionRectAboutPoint(RectTransform rectTransform, Vector2 point, Canvas canvas)
    {
        rectTransform.position = point;
        //x 0-1 is l-r, y 0-1 is b-t
        var pivot = Vector2.up; //default top left corner
        if (point.x + rectTransform.rect.width * canvas.scaleFactor > canvas.pixelRect.width) pivot.x = 1;
        if (point.y - rectTransform.rect.height * canvas.scaleFactor < 0) pivot.y = 0;
        rectTransform.pivot = pivot;
    }
}

using UnityEngine;

namespace Utility.Scripts
{
    public static class RectTransformHelpers
    {
        public static void SetOffset(this RectTransform rectTransform, Vector4 value)
        {
            rectTransform.offsetMin = new Vector2(value.x, value.y);
            rectTransform.offsetMax = new Vector2(value.z, value.w);
        }
    }
}
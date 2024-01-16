using System;
using UnityEngine;
using UnityEngine.UI;
using Utility.Scripts;

public class RectOffsetSpringListener : Vector4SpringListener
{
    [SerializeField] private RectTransform targetRect;

    protected override Vector4 GetOrig()
    {
        return new Vector4(targetRect.offsetMin.x, targetRect.offsetMin.y, targetRect.offsetMax.x,
            targetRect.offsetMax.y);
    }

    protected override void ChangeValue(Vector4 value)
    {
        value = Vector4.Scale(value, new Vector4(1, 1, -1, -1));
        targetRect.SetOffset(value);
    }
}

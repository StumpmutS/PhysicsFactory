using UnityEngine;
using Utility.Scripts;

public class ToggleRectOffsetChanger : ToggleChanger
{
    [SerializeField] private RectTransform targetRect;
    [SerializeField, Tooltip("x: Left, y: Bottom, z: Right, w: Top")] private Vector4 onOffset, offOffset;
    
    protected override void ChangeValue(bool value)
    {
        var offset = value ? onOffset : offOffset;
        offset = Vector4.Scale(offset, new Vector4(1, 1, -1, -1));
        targetRect.SetOffset(offset);
    }
}
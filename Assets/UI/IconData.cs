using System;
using UnityEngine;

[Serializable]
public class IconData
{
    [SerializeField] private Sprite sprite;
    public Sprite Sprite => sprite;
    [SerializeField] private ColorSelectionWrapper colorWrapper;
    public Color Color => colorWrapper.Color;

    public IconData(Sprite sprite, Color color)
    {
        this.sprite = sprite;
        colorWrapper = new ColorSelectionWrapper(color);
    }
}

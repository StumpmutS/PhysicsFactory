using System;
using UnityEngine;

[Serializable]
public class IconInfo
{
    [SerializeField] private Sprite sprite;
    public Sprite Sprite => sprite;
    [SerializeField] private Color color;
    public Color Color => color;

    public IconInfo(Sprite sprite, Color color)
    {
        this.sprite = sprite;
        this.color = color;
    }
}
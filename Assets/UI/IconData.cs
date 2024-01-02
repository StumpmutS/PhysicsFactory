using System;
using UnityEngine;

[Serializable]
public class IconData
{
    [SerializeField] private Sprite sprite;
    public Sprite Sprite => sprite;
    [SerializeField] private Color color;
    public Color Color => color;

    public IconData(Sprite sprite, Color color)
    {
        this.sprite = sprite;
        this.color = color;
    }
}
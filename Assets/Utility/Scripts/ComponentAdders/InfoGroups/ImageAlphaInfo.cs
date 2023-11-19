using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ImageAlphaInfo
{
    [SerializeField] private Image image;
    public Image Image => image;
    [SerializeField] private float floatValue;
    public float Float => floatValue;
}
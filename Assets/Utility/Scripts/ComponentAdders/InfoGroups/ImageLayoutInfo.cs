using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ImageLayoutInfo
{
    [SerializeField] private LayoutDisplay layoutPrefab;
    public LayoutDisplay LayoutPrefab => layoutPrefab;
    [SerializeField] private Image imagePrefab;
    public Image ImagePrefab => imagePrefab;
}
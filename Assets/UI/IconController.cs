using System;
using UnityEngine;
using UnityEngine.UI;

public class IconController : MonoBehaviour
{
    [SerializeField] private Image image;

    private IconData _defaultIcon;
    
    private void Awake()
    {
        _defaultIcon = new IconData(image.sprite, image.color);
    }
    
    public void SetIcon(IconData data)
    {
        image.sprite = data.Sprite;
        image.color = data.Color;
    }

    public void SetIcon(IconSO so) => SetIcon(so.Icon);

    public void ResetIcon() => SetIcon(_defaultIcon);
}

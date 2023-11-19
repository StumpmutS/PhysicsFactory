using UnityEngine;
using UnityEngine.UI;

public class ToggleIconChanger : ToggleChanger
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite toggledSprite;
    [SerializeField] private Sprite untoggledSprite;
    
    protected override void ChangeValue(bool value)
    {
        image.sprite = value ? toggledSprite : untoggledSprite;
    }
}
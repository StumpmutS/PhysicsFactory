using UnityEngine;
using UnityEngine.UI;
using Utility.Scripts;

public class BlockableIcon : MonoBehaviour, IUIBlockable, IInitializableComponent<Image>
{
    [SerializeField] private Image icon;

    private IconInfo _defaultIconInfo;
    
    public void Init(Image imagePrefab)
    {
        icon = Instantiate(imagePrefab, transform, false);
    }
    
    public void Block(UIBlockableInfo info)
    {
        _defaultIconInfo = new IconInfo(icon.sprite, icon.color);
        SetIcon(info.MainIcon);
    }

    public void Unblock()
    {
        SetIcon(_defaultIconInfo);
    }

    private void SetIcon(IconInfo info)
    {
        icon.sprite = info.Sprite;
        icon.color = info.Color;
    }
}
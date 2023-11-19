using UnityEngine;
using UnityEngine.UI;
using Utility.Scripts;

public class BlockableAlphaCover : MonoBehaviour, IUIBlockable, IInitializableComponent<ImageAlphaInfo>
{
    [SerializeField] private Image image;
    [SerializeField] private float blockedAlpha;

    private float _defaultAlpha;

    public void Init(ImageAlphaInfo info)
    {
        image = Instantiate(info.Image, transform, false);
        blockedAlpha = info.Float;
    }
    
    public void Block(UIBlockableInfo info)
    {
        var color = image.color;
        _defaultAlpha = color.a;
        color.a = blockedAlpha;
        image.color = color;
    }

    public void Unblock()
    {
        if (image == null) return;
        
        var color = image.color;
        color.a = _defaultAlpha;
        image.color = color;
    }
}
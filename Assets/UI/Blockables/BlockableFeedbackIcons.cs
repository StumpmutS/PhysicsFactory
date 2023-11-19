using UnityEngine;
using UnityEngine.UI;
using Utility.Scripts;

public class BlockableFeedbackIcons : MonoBehaviour, IUIBlockable, IInitializableComponent<ImageLayoutInfo>
{
    [SerializeField] private LayoutDisplay layout;
    [SerializeField] private Image iconPrefab;

    public void Init(ImageLayoutInfo info)
    {
        layout = Instantiate(info.LayoutPrefab, transform, false);
        iconPrefab = info.ImagePrefab;
    }
    
    public void Block(UIBlockableInfo info)
    {
        layout.Clear();
        
        foreach (var iconInfo in info.FeedbackIcons)
        {
            var image = Instantiate(iconPrefab);
            image.sprite = iconInfo.Sprite;
            image.color = iconInfo.Color;
            layout.Add((RectTransform) image.transform);
        }
    }

    public void Unblock()
    {
        layout.Clear();
    }
}
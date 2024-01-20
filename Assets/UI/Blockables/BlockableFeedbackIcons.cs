using UnityEngine;
using UnityEngine.UI;
using Utility.Scripts;

public class BlockableFeedbackIcons : MonoBehaviour, IUIBlockable
{
    [SerializeField] private LayoutDisplay layout;
    [SerializeField] private Image iconPrefab;

    public void Init(ImageLayoutInfo info)
    {
        if (layout == null) layout = Instantiate(info.LayoutPrefab, transform, false);
        iconPrefab = info.ImagePrefab;
    }
    
    public void Block(UIBlockableData data)
    {
        layout.Clear();
        
        foreach (var iconInfo in data.FeedbackIcons)
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
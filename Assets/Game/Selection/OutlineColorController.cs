using UnityEngine;

public class OutlineColorController : MonoBehaviour
{
    [SerializeField] private ColorInfo colors;
    [SerializeField] private Outline outline;
    [SerializeField] private Selectable selectable;

    private void Awake()
    {
        selectable.OnHover.AddListener(HandleOutline);
        selectable.OnHoverStop.AddListener(HandleOutline);
        selectable.OnSelect.AddListener(HandleOutline);
        selectable.OnDeselect.AddListener(HandleOutline);
        selectable.OnEngage.AddListener(HandleOutline);
        selectable.OnDisengage.AddListener(HandleOutline);
    }
    
    private void HandleOutline(Selectable _)
    {
        SetColor(DetermineColor());
    }

    private void SetColor(Color color)
    {
        outline.OutlineColor = color;
    }

    private Color DetermineColor()
    {
        if (selectable.Selected && selectable.Engaged) return colors.Colors[2];
        return selectable.Engaged ? colors.Colors[1] : colors.Colors[0];
    }

    private void OnDestroy()
    {
        if (selectable != null) selectable.OnHover.AddListener(HandleOutline);
        if (selectable != null) selectable.OnHoverStop.AddListener(HandleOutline);
        if (selectable != null) selectable.OnSelect.AddListener(HandleOutline);
        if (selectable != null) selectable.OnDeselect.AddListener(HandleOutline);
        if (selectable != null) selectable.OnEngage.AddListener(HandleOutline);
        if (selectable != null) selectable.OnDisengage.AddListener(HandleOutline);
    }
}

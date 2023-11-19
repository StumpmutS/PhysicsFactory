using UnityEngine;
using UnityEngine.UI;
using Utility.Scripts;

public class LayoutDrivenToggleGroup : MonoBehaviour
{
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private LayoutDisplay layoutDisplay;

    private void Awake()
    {
        layoutDisplay.OnAdd.AddListener(HandleAdd);
    }

    private void HandleAdd(RectTransform rectTransform)
    {
        if (rectTransform.TryGetComponentInChildren<Toggle>(out var toggle)) toggle.group = toggleGroup;
    }

    private void OnDestroy()
    {
        if (layoutDisplay != null) layoutDisplay.OnAdd.RemoveListener(HandleAdd);
    }
}
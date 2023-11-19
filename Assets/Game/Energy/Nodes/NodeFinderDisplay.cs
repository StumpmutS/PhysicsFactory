using UnityEngine;
using Utility.Scripts;

public class NodeFinderDisplay : SelectableDisplay<EnergyNodeFinder>
{
    [SerializeField] private GameObject container;
    [SerializeField] private SignedFloatSelector floatSelectorPrefab;
    [SerializeField] private LayoutDisplay layout;
    [SerializeField] private string sizeLabel = "Size";

    protected override void SetupSelectionDisplay(EnergyNodeFinder nodeFinder)
    {
        container.SetActive(true);
        var selector = CreateFloatSelector(nodeFinder);
        selector.OnChanged.AddListener(HandleSelectorChanged);
        if (selector.transform is not RectTransform rectTransform) return;
        layout.Add(rectTransform);
    }

    private void HandleSelectorChanged(object obj, SignedFloat value)
    {
        if (obj is not EnergyNodeFinder nodeFinder) return;
        
        nodeFinder.Range = value.Value;
    }

    private SignedFloatSelector CreateFloatSelector(EnergyNodeFinder nodeFinder)
    {
        var selector = Instantiate(floatSelectorPrefab);
        selector.Init(new SignedFloat(nodeFinder.Range, true), nodeFinder, nodeFinder.MaxRange);
        Label.SetLabel(selector, sizeLabel);
        return selector;
    }

    protected override void RemoveSelectionDisplay()
    {
        container.SetActive(false);
        layout.Clear();
    }
}
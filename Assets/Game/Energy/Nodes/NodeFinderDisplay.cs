using UnityEngine;
using Utility.Scripts;

public class NodeFinderDisplay : SelectableDisplay<EnergyNodeFinder>
{
    [SerializeField] private GameObject container;
    [SerializeField] private SignedFloatSelector floatSelectorPrefab;
    [SerializeField] private LayoutDisplay layout;
    [SerializeField] private string sizeLabel = "Size";

    private EnergyNodeFinder _nodeFinder;
    private SignedFloatSelector _selector;
    
    protected override void SetupSelectionDisplay(EnergyNodeFinder nodeFinder)
    {
        container.SetActive(true);
        _nodeFinder = nodeFinder;
        _nodeFinder.OnMaxRangeUpdated.AddListener(HandleMaxRangeUpdate);
        _selector = CreateFloatSelector(_nodeFinder);
        _selector.OnChanged.AddListener(HandleSelectorChanged);
        if (_selector.transform is not RectTransform rectTransform) return;
        layout.Add(rectTransform);
    }

    private void HandleMaxRangeUpdate(float newMaxRange)
    {
        _selector.UpdateMax(newMaxRange);
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
        if (_nodeFinder != null) _nodeFinder.OnMaxRangeUpdated.RemoveListener(HandleMaxRangeUpdate);
        container.SetActive(false);
        layout.Clear();
    }
}
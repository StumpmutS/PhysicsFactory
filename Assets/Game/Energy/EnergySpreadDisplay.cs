using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utility.Scripts;

public class EnergySpreadDisplay : SelectableDisplay<EnergySpreadController>
{
    [SerializeField] private GameObject container;
    [SerializeField] private LayoutDisplay layout;
    [SerializeField] private TMP_Text text;
    [SerializeField] private SignedFloatSelector signedIntegerSelectorPrefab;

    private EnergySpreadController _controller;
    private Dictionary<IEnergySpender, SignedFloatSelector> _selectors = new();

    protected override void SetupSelectionDisplay(Selectable selectable, EnergySpreadController controller)
    {
        container.SetActive(true);
        _controller = controller;
        SetText(controller.Spenders.CurrentTotal, controller.Spenders.MaxTotal);
        selectable.OnDeselect.AddListener(RemoveSelectionDisplay);
        controller.Spenders.OnFloatsChanged += HandleSpendersChanged;
        foreach (var kvp in controller.Spenders.Floats)
        {
            SetupNewSelector(kvp.Key, kvp.Value);
        }
    }

    private void SetText(float currentTotal, float maxTotal)
    {
        text.text = $"Charge Spent: {currentTotal:F2} / {maxTotal:F2}";
    }

    private void SetupNewSelector(IEnergySpender spender, SignedFloat value)
    {
        var selector = CreateFloatSelector(spender, value);
        _selectors[spender] = selector;
        layout.Add(selector.transform);
    }

    private SignedFloatSelector CreateFloatSelector(object callbackObj, SignedFloat value)
    {
        var selector = Instantiate(signedIntegerSelectorPrefab);
        selector.Init(value, callbackObj);
        selector.OnChanged += HandleSelectorChanged;
        return selector;
    }

    private void HandleSpendersChanged()
    {
        foreach (var kvp in _controller.Spenders.Floats)
        {
            if (_selectors.TryGetValue(kvp.Key, out var selector))
            {
                selector.UpdateVisuals(kvp.Value);
            }
            else
            {
                SetupNewSelector(kvp.Key, kvp.Value);
            }
        }
        
        SetText(_controller.Spenders.CurrentTotal, _controller.Spenders.MaxTotal);
    }

    private void HandleSelectorChanged(object callbackObj, SignedFloat value)
    {
        if (callbackObj is not IEnergySpender spender) return;
        _controller.Spenders.SetValue(spender, value);
    }

    protected override void RemoveSelectionDisplay(Selectable selectable)
    {
        container.SetActive(false);
        layout.Clear();
    }
}
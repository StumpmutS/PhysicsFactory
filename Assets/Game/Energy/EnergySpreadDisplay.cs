using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;

public class EnergySpreadDisplay : Singleton<EnergySpreadDisplay>
{
    [SerializeField] private LayoutDisplay layout;
    [SerializeField] private SignedFloatSelector signedIntegerSelectorPrefab;

    private EnergySpreadController _controller;
    private Dictionary<IEnergySpender, SignedFloatSelector> _selectors = new();

    private void Start()
    {
        SelectionEvents.Instance.OnSelected.AddListener(HandleSelection);
    }

    private void HandleSelection(Selectable selectable)
    {
        if (selectable.MainObject.TryGetComponent<EnergySpreadController>(out var controller))
        {
            SetupDisplay(selectable, controller);
        }
    }

    private void SetupDisplay(Selectable selectable, EnergySpreadController controller)
    {
        _controller = controller;
        selectable.OnDeselect.AddListener(RemoveDisplay);
        controller.Spenders.OnFloatsChanged += HandleSpendersChanged;
        foreach (var kvp in controller.Spenders.Floats)
        {
            var selector = CreateFloatSelector(kvp.Key, kvp.Value);
            _selectors[kvp.Key] = selector;
            layout.Add(selector.transform);
        }
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
            _selectors[kvp.Key].UpdateVisuals(kvp.Value);
        }
    }

    private void HandleSelectorChanged(object callbackObj, SignedFloat value)
    {
        if (callbackObj is not IEnergySpender spender) return;
        _controller.Spenders.SetValue(spender, value);
    }

    private void RemoveDisplay(Selectable selectable)
    {
        selectable.OnDeselect.RemoveListener(RemoveDisplay);
        layout.Clear();
    }
}
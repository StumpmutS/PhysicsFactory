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
            SetupNewSelector(kvp.Key, kvp.Value);
        }
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
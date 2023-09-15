using System;
using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;

public class ModificationDisplay : SelectableDisplay<ModificationContainer>
{
    [SerializeField] private GameObject container;
    [SerializeField] private LayoutDisplay layout;
    [SerializeField] private LabeledCallbackButton buttonPrefab;

    private ModificationContainer _modificationContainer;
    private Dictionary<ModificationData, LabeledCallbackButton> _buttons = new();

    protected override void SetupSelectionDisplay(ModificationContainer modificationContainer)
    {
        container.SetActive(true);
        _buttons.Clear();
        _modificationContainer = modificationContainer;
        foreach (var kvp in modificationContainer.Modifications)
        {
            var button = CreateButton(kvp.Key, kvp.Value);
            _buttons[kvp.Key] = button;
            layout.Add(button.transform);
        }
    }

    private LabeledCallbackButton CreateButton(ModificationData modificationData, bool active)
    {
        var button = Instantiate(buttonPrefab);
        SetButton(button, modificationData, active);
        return button;
    }
    
    private void SetButton(LabeledCallbackButton button, ModificationData modData, bool active)
    {
        Set(active ? HandleSellPressed : HandleActivatePressed, active ? "Sell " : "Buy ",
            ": $" + (active
                ? SupplyCalculator.CalculatePrice(modData.Info.Price, _modificationContainer.ModifiedBuilding, modData.Info.SaleMultiplier)
                : SupplyCalculator.CalculatePrice(modData.Info.Price, _modificationContainer.ModifiedBuilding))
            .ToString("F2"));
        
        void Set(Action<object> action, string pre, string post)
        {
            button.Init(action, modData);
            button.SetText(pre + modData.Info.Label + post);
        }
    }

    private void HandleActivatePressed(object callbackObj)
    {
        if (callbackObj is not ModificationData modification) return;
        _modificationContainer.TryActivateModification(modification);
        SetButton(_buttons[modification], modification, _modificationContainer.Modifications[modification]);
    }

    private void HandleSellPressed(object callbackObj)
    {
        if (callbackObj is not ModificationData modification) return;
        _modificationContainer.TrySellModification(modification);
        SetButton(_buttons[modification], modification, _modificationContainer.Modifications[modification]);
    }
    
    protected override void RemoveSelectionDisplay()
    {
        container.SetActive(false);
        layout.Clear();
    }
}
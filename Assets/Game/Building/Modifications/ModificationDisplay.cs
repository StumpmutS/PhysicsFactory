using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;

public class ModificationDisplay : SelectableDisplay<ModificationContainer>
{
    [SerializeField] private LayoutDisplay layout;
    [SerializeField] private LabeledCallbackButton buttonPrefab;

    private ModificationContainer _container;
    private Dictionary<ModificationData, LabeledCallbackButton> _buttons = new();

    protected override void SetupSelectionDisplay(Selectable selectable, ModificationContainer container)
    {
        _buttons.Clear();
        _container = container;
        selectable.OnDeselect.AddListener(RemoveSelectionDisplay);
        foreach (var kvp in container.Modifications)
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
    
    private void SetButton(LabeledCallbackButton button, ModificationData modificationData, bool active)
    {
        button.Init(active ? HandleSellPressed : HandleActivatePressed, modificationData);
        button.SetText((active ? "Sell " : "") + modificationData.Info.Label);
    }

    private void HandleActivatePressed(object callbackObj)
    {
        if (callbackObj is not ModificationData modification) return;
        _container.TryActivateModification(modification);
        SetButton(_buttons[modification], modification, _container.Modifications[modification]);
    }

    private void HandleSellPressed(object callbackObj)
    {
        if (callbackObj is not ModificationData modification) return;
        _container.TrySellModification(modification);
        SetButton(_buttons[modification], modification, _container.Modifications[modification]);
    }
    
    protected override void RemoveSelectionDisplay(Selectable selectable)
    {
        layout.Clear();
    }
}
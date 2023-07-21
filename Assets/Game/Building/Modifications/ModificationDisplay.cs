using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;

public class ModificationDisplay : Singleton<ModificationDisplay>
{
    [SerializeField] private LayoutDisplay layout;
    [SerializeField] private LabeledCallbackButton buttonPrefab;

    private ModificationContainer _container;
    private Dictionary<ModificationData, LabeledCallbackButton> _buttons = new();

    private void Start()
    {
        SelectionEvents.Instance.OnSelected.AddListener(HandleSelection);
    }

    private void HandleSelection(Selectable selectable)
    {
        if (selectable.MainObject.TryGetComponent<ModificationContainer>(out var container))
        {
            SetupDisplay(selectable, container);
        }
    }

    private void SetupDisplay(Selectable selectable, ModificationContainer container)
    {
        _buttons.Clear();
        _container = container;
        selectable.OnDeselect.AddListener(RemoveDisplay);
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
    
    private void RemoveDisplay(Selectable selectable)
    {
        selectable.OnDeselect.RemoveListener(RemoveDisplay);
        layout.Clear();
    }
}
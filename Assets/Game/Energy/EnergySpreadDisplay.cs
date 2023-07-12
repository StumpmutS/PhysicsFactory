using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;

public class EnergySpreadDisplay : MonoBehaviour
{
    [SerializeField] private LayoutDisplay layout;
    [SerializeField] private SignedIntegerSelector signedIntegerSelectorPrefab;

    private EnergySpreadController _controller;
    
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
        foreach (var kvp in controller.Spenders.Integers)
        {
            layout.Add(CreateIntegerSelector(kvp.Key, kvp.Value).transform);
        }
    }

    private SignedIntegerSelector CreateIntegerSelector(object callbackObj, SignedInt value)
    {
        var selector = Instantiate(signedIntegerSelectorPrefab);
        selector.Init(value, callbackObj);
        selector.OnChanged += HandleSelectorChanged;
        return selector;
    }

    private void HandleSelectorChanged(object callbackObj, SignedInt value)
    {
        if (callbackObj is not IEnergySpender spender) return;
        _controller.Spenders.SetValue(spender, value.AsInt());
    }

    private void RemoveDisplay(Selectable selectable)
    {
        selectable.OnDeselect.RemoveListener(RemoveDisplay);
        layout.Clear();
    }
}
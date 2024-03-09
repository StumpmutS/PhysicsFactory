using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Utility.Scripts;

public class EnergySpreadDisplay : SelectableDisplay<EnergySpreadController>
{
    [SerializeField] private GameObject container;
    [SerializeField] private LayoutDisplay layout;
    [SerializeField] private TMP_Text text;
    [SerializeField] private SignedFloatSelector floatSelectorPrefab;
    [SerializeField] private ContextualUIObjectData contextualUIObjectData;

    private EnergySpreadController _controller;
    private Dictionary<IChargeable, SignedFloatSelector> _selectors = new();

    protected override void SetupSelectionDisplay(EnergySpreadController controller)
    {
        container.SetActive(true);
        _controller = controller;
        controller.OnSpendersChanged.AddListener(RefreshSelectors);
        SetText(controller.CurrentTotal, controller.MaxTotal);
        foreach (var spender in controller.Spenders)
        {
            SetupNewSelector(spender, spender.ChargePacket.CurrentCharge);
        }
    }
    
    private void SetText(float currentTotal, float maxTotal)
    {
        text.text = $"Charge Spent: {currentTotal:F2} / {maxTotal:F2}";
    }

    private void SetupNewSelector(IChargeable spender, SignedFloat value)
    {
        var selector = CreateFloatSelector(spender, value, GenerateContext(spender.Context, value));
        _selectors[spender] = selector;
        if (selector.transform is not RectTransform rectTransform) return;
        layout.Add(rectTransform);
    }

    private SignedFloatSelector CreateFloatSelector(object callbackObj, SignedFloat value, ContextData context)
    {
        var selector = Instantiate(floatSelectorPrefab);
        selector.Init(value, callbackObj);
        ContextualUIObjectBuilder.BuildObject(selector.gameObject, contextualUIObjectData, context);
        selector.OnChanged.AddListener(HandleSelectorChanged);
        return selector;
    }

    private void RefreshSelectors()
    {
        foreach (var spender in _controller.Spenders)
        {
            if (_selectors.TryGetValue(spender, out var selector))
            {
                selector.SignedFloat = spender.ChargePacket.CurrentCharge;
                if (!selector.TryGetComponent<ContextDataContainer>(out var contextContainer)) continue;
                
                contextContainer.SetData(GenerateContext(spender.Context, spender.ChargePacket.CurrentCharge));
            }
            else
            {
                SetupNewSelector(spender, spender.ChargePacket.CurrentCharge);
            }
        }

        var selectorsCopy = _selectors.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        foreach (var kvp in selectorsCopy)
        {
            if (_controller.Spenders.Contains(kvp.Key)) continue;
            
            Destroy(kvp.Value.gameObject);
            _selectors.Remove(kvp.Key);
        }
        
        SetText(_controller.CurrentTotal, _controller.MaxTotal);
    }

    private void HandleSelectorChanged(object callbackObj, SignedFloat value)
    {
        if (callbackObj is not IChargeable spender) return;
        spender.ChargePacket.UpdateRequestedCharge(value);
        RefreshSelectors();
    }

    private ContextData GenerateContext(ContextData context, SignedFloat value)
    {
        return new ContextData(context.Label, $"{value.Value:F2}/{_controller.MaxTotal:F2}");
    }

    protected override void RemoveSelectionDisplay()
    {
        if (_controller != null) _controller.OnSpendersChanged.RemoveListener(RefreshSelectors);
        container.SetActive(false);
        layout.Clear();
        _selectors.Clear();
    }
}
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
    [SerializeField] private LabeledSignedFloatSelector floatSelectorPrefab;

    private EnergySpreadController _controller;
    private Dictionary<IEnergySpender, LabeledSignedFloatSelector> _selectors = new();

    protected override void SetupSelectionDisplay(EnergySpreadController controller)
    {
        container.SetActive(true);
        _controller = controller;
        SetText(controller.Spenders.CurrentTotal, controller.Spenders.MaxTotal);
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
        var selector = CreateFloatSelector(spender, value, spender.SpenderInfo.Label);
        _selectors[spender] = selector;
        layout.Add(selector.transform);
    }

    private LabeledSignedFloatSelector CreateFloatSelector(object callbackObj, SignedFloat value, string label)
    {
        var selector = Instantiate(floatSelectorPrefab);
        selector.Init(value, callbackObj);
        selector.SetLabel(label);
        selector.OnChanged.AddListener(HandleSelectorChanged);
        return selector;
    }

    private void HandleSpendersChanged()
    {
        var floatsCopy = _controller.Spenders.Floats.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        foreach (var kvp in floatsCopy)
        {
            if (_selectors.TryGetValue(kvp.Key, out var selector))
            {
                selector.SignedFloat = kvp.Value;
                selector.SetLabel(kvp.Key.SpenderInfo.Label);
            }
            else
            {
                SetupNewSelector(kvp.Key, kvp.Value);
            }
        }

        var selectorsCopy = _selectors.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        foreach (var kvp in selectorsCopy)
        {
            if (_controller.Spenders.Floats.ContainsKey(kvp.Key)) continue;
            Destroy(kvp.Value.gameObject);
            _selectors.Remove(kvp.Key);
        }
        
        SetText(_controller.Spenders.CurrentTotal, _controller.Spenders.MaxTotal);
    }

    private void HandleSelectorChanged(object callbackObj, SignedFloat value)
    {
        if (callbackObj is not IEnergySpender spender) return;
        _controller.Spenders.SetValue(spender, value);
    }

    protected override void RemoveSelectionDisplay()
    {
        container.SetActive(false);
        layout.Clear();
        _selectors.Clear();
        if (_controller != null) _controller.Spenders.OnFloatsChanged -= HandleSpendersChanged;
    }
}
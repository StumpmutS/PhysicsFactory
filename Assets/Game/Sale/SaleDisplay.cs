﻿using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility.Scripts.Extensions;

public class SaleDisplay : SelectableDisplay<SaleController>
{
    [SerializeField] private GameObject container;
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text text;

    private SaleController _saleController;
    private DataService<ContextData> _contextDataService;

    private void Awake()
    {
        button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        if (_saleController == null) return;

        _saleController.Sell();
    }

    protected override void SetupSelectionDisplay(SaleController saleController)
    {
        saleController.AddOrGetComponent<GeneralRefreshEvent>().OnRefresh.AddListener(RefreshDisplay);
        container.SetActive(true);
        _saleController = saleController;
        button.gameObject.SetActive(true);
        
        RefreshDisplay();
    }

    private void RefreshDisplay()
    {
        var stringBuilder = new StringBuilder("Sell");
        if (_saleController.TryGetComponent<DataService<ContextData>>(out _contextDataService))
        {
            stringBuilder.Append($" {_contextDataService.RequestData().Label}");
            _contextDataService.OnUpdated.AddListener(HandleServiceUpdate);
        }
        stringBuilder.Append($": ${_saleController.SalePriceSummation:F2}");
        
        text.text = stringBuilder.ToString();
    }

    private void HandleServiceUpdate(ContextData data)
    {
        if (_contextDataService != null) _contextDataService.OnUpdated.RemoveListener(HandleServiceUpdate);
        _contextDataService = null;
        RefreshDisplay();
    }

    protected override void RemoveSelectionDisplay()
    {
        container.SetActive(false);
        button.gameObject.SetActive(false);
    }
}
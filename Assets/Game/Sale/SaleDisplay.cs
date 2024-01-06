using System.Text;
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
        if (_saleController.TryGetComponent<ContextDataContainer>(out var contextDataContainer))
        {
            stringBuilder.Append($" {contextDataContainer.Data.Label}");
        }
        stringBuilder.Append($": ${_saleController.SalePriceSummation:F2}");
        
        text.text = stringBuilder.ToString();
    }

    protected override void RemoveSelectionDisplay()
    {
        container.SetActive(false);
        button.gameObject.SetActive(false);
    }
}
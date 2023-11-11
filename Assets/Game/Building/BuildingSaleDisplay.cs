using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSaleDisplay : SelectableDisplay<BuildingSaleController>
{
    [SerializeField] private GameObject container;
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text text;

    private BuildingSaleController _saleController;
    
    private void Awake()
    {
        button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        if (_saleController == null) return;

        _saleController.Sell();
    }

    protected override void SetupSelectionDisplay(BuildingSaleController saleController)
    {
        container.SetActive(true);
        _saleController = saleController;
        button.gameObject.SetActive(true);
        text.text = _saleController.SaleText;
    }
    
    protected override void RemoveSelectionDisplay()
    {
        container.SetActive(false);
        button.gameObject.SetActive(false);
    }
}
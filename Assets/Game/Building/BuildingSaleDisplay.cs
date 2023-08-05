using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility.Scripts;

public class BuildingSaleDisplay : SelectableDisplay<BuildingSaleController>
{
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

    protected override void SetupSelectionDisplay(Selectable selectable, BuildingSaleController saleController)
    {
        _saleController = saleController;
        button.gameObject.SetActive(true);
        text.text = _saleController.SaleText;
    }
    
    protected override void RemoveSelectionDisplay(Selectable selectable)
    {
        button.gameObject.SetActive(false);
    }
}
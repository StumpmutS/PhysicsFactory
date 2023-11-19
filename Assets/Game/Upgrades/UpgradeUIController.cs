using UnityEngine;
using UnityEngine.Events;
using Utility.Scripts;

public class UpgradeUIController : MonoBehaviour
{
    [SerializeField] private SignedIntegerSelector integerSelector;
    [SerializeField] private Label levelLabel;
    [SerializeField] private Label upgradePriceLabel;
    [SerializeField] private Label downgradePriceLabel;

    private Upgradeable _upgradeable;
    
    public UnityEvent<Upgradeable, int> OnChanged = new();
    
    public void Init(Upgradeable upgradeable)
    {
        _upgradeable = upgradeable;
        _upgradeable.OnUpgrade.AddListener(Refresh);
        _upgradeable.OnDowngrade.AddListener(Refresh);
        integerSelector.Init(new SignedInt(upgradeable.Level, true), upgradeable, upgradeable.MaxLevel);
        Refresh();
    }
    
    private void Start()
    {
        integerSelector.OnChanged.AddListener(HandleSelectorChanged);
    }

    private void HandleSelectorChanged(object obj, SignedInt signedInt)
    {
        if (obj is not Upgradeable upgradeable) return;
        
        OnChanged.Invoke(upgradeable, signedInt.AsInt());
        Refresh();
    }

    private void Refresh()
    {
        integerSelector.SignedInt = new SignedInt(_upgradeable.Level, true);
        levelLabel.SetLabel($"Level: {_upgradeable.Level}");
        var upgradePrice = _upgradeable.UpgradePrice;
        var downgradePrice = _upgradeable.DowngradePrice;
        upgradePriceLabel.SetLabel($"${(upgradePrice >= 0 ? upgradePrice.ToString("F2") : "-")}");
        downgradePriceLabel.SetLabel($"${(downgradePrice >= 0 ? downgradePrice.ToString("F2") : "-")}");
    }

    private void OnDestroy()
    {
        if (_upgradeable != null) _upgradeable.OnUpgrade.RemoveListener(Refresh);
        if (_upgradeable != null) _upgradeable.OnDowngrade.RemoveListener(Refresh);
        if (integerSelector != null) integerSelector.OnChanged.RemoveListener(HandleSelectorChanged);
    }
}
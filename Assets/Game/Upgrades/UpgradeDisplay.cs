using System;
using TMPro;
using UnityEngine;
using Utility.Scripts;

public class UpgradeDisplay : SelectableDisplay<Upgradeable>
{
    [SerializeField] private GameObject container;
    [SerializeField] private UpgradeUIController upgradeUIController;
    [SerializeField] private LayoutDisplay layout;
    
    protected override void SetupSelectionDisplay(Upgradeable upgradeable)
    {
        container.SetActive(true);
        var controller = Instantiate(upgradeUIController);
        controller.Init(upgradeable);
        controller.OnChanged.AddListener(HandleUIControllerChanged);
        if (controller.transform is not RectTransform rectTransform) return;
        
        layout.Add(rectTransform);
    }

    private void HandleUIControllerChanged(Upgradeable upgradeable, int desiredLevel)
    {
        while (upgradeable.Level != desiredLevel)
        {
            if (upgradeable.Level < desiredLevel && !TryUpgrade(upgradeable)) break;
            if (upgradeable.Level > desiredLevel && !TryDowngrade(upgradeable)) break; 
        }
    }

    private bool TryUpgrade(Upgradeable upgradeable)
    {
        var failureInfo = new RestrictionFailureInfo();
        if (upgradeable.TryUpgrade(failureInfo)) return true;
        
        RestrictionFailureDisplay.Instance.DisplayFailure(failureInfo);
        return false;
    }

    private bool TryDowngrade(Upgradeable upgradeable)
    {
        var failureInfo = new RestrictionFailureInfo();
        if (upgradeable.TryDowngrade(failureInfo)) return true;
        
        RestrictionFailureDisplay.Instance.DisplayFailure(failureInfo);
        return false;
    }

    protected override void RemoveSelectionDisplay()
    {
        container.SetActive(false);
        layout.Clear();
    }
}
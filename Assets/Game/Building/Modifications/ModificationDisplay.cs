using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;
using Utility.Scripts.Extensions;

public class ModificationDisplay : SelectableDisplay<ModificationContainer>
{
    [SerializeField] private GameObject container;
    [SerializeField] private LayoutDisplay layout;
    [SerializeField] private LabeledCallbackToggle togglePrefab;
    [SerializeField] private IconData restrictedIcon;

    private ModificationContainer _modificationContainer;
    private GeneralRefreshEvent _generalRefreshEvent;
    private Dictionary<ModificationData, LabeledCallbackToggle> _toggles = new();

    protected override void SetupSelectionDisplay(ModificationContainer modificationContainer)
    {
        container.SetActive(true);
        _toggles.Clear();
        _modificationContainer = modificationContainer;
        _generalRefreshEvent = _modificationContainer.AddOrGetComponent<GeneralRefreshEvent>();
        _generalRefreshEvent.OnRefresh.AddListener(GeneralRefresh);
        foreach (var kvp in modificationContainer.ModificationsActive)
        {
            var toggle = CreateToggle(kvp.Key, kvp.Value);
            _toggles[kvp.Key] = toggle;
            if (toggle.transform is not RectTransform rectTransform) return;
            layout.Add(rectTransform);
        }
        GeneralRefresh();
    }

    private LabeledCallbackToggle CreateToggle(ModificationData modificationData, bool active)
    {
        var toggle = Instantiate(togglePrefab);
        SetToggle(toggle, modificationData, active);
        return toggle;
    }
    
    private void SetToggle(LabeledCallbackToggle toggle, ModificationData modData, bool active)
    {
        toggle.Init(new CallbackToggleData(HandleToggle, modData, active));
        toggle.SetText(modData.Info.Label);
    }

    private void HandleToggle(object callbackObj, bool value)
    {
        if (callbackObj is not ModificationData modification) return;
        if (value) HandleActivatePressed(modification);
        else HandleDeactivatePressed(modification);
    }

    private void HandleActivatePressed(ModificationData modData)
    {
        var failureInfo = new RestrictionFailureInfo();
        if (!_modificationContainer.TryActivateModification(modData, failureInfo))
        {
            RestrictionFailureDisplay.Instance.DisplayFailure(failureInfo);
        }
        SetToggle(_toggles[modData], modData, _modificationContainer.ModificationsActive[modData]);
    }

    private void HandleDeactivatePressed(ModificationData modData)
    {
        _modificationContainer.TryDeactivateModification(modData);
        SetToggle(_toggles[modData], modData, _modificationContainer.ModificationsActive[modData]);
    }

    private void GeneralRefresh()
    {
        foreach (var kvp in _toggles)
        {
            var restrictionInfo = ModificationHelpers.GenerateRestrictionInfo(_modificationContainer.ModifiedBuilding, kvp.Key.Info);
            var failureInfo = new RestrictionFailureInfo();
            if (RestrictionHelper.CheckRestrictions(kvp.Key.Info.ActivationRestrictions, restrictionInfo, failureInfo))
            {
                kvp.Value.Toggle.RemoveOneComponent<RestrictionUIBlocker>();
                continue;
            }
            
            var blocker = kvp.Value.Toggle.AddOrGetComponent<RestrictionUIBlocker>();
            blocker.Init(failureInfo.FailureType, restrictedIcon.Icon);
            kvp.Value.Toggle.isOn = false;
        }
    }
    
    protected override void RemoveSelectionDisplay()
    {
        if (_generalRefreshEvent != null) _generalRefreshEvent.OnRefresh.RemoveListener(GeneralRefresh);
        container.SetActive(false);
        layout.Clear();
    }
}

using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;
using Utility.Scripts.Extensions;

public class ModificationDisplay : SelectableDisplay<ModificationContainer>
{
    [SerializeField] private GameObject container;
    [SerializeField] private LayoutDisplay layout;
    [SerializeField] private CallbackToggle togglePrefab;
    [SerializeField] private ContextualUIObjectData toggleContextualData;
    [SerializeField] private GameObject restrictedBlockingPrefab;

    private ModificationContainer _modificationContainer;
    private GeneralRefreshEvent _generalRefreshEvent;
    private Dictionary<AssetRefContainer<ModificationSO>, CallbackToggle> _toggles = new();

    protected override void SetupSelectionDisplay(ModificationContainer modificationContainer)
    {
        container.SetActive(true);
        _toggles.Clear();
        _modificationContainer = modificationContainer;
        _generalRefreshEvent = _modificationContainer.AddOrGetComponent<GeneralRefreshEvent>();
        _generalRefreshEvent.OnRefresh.AddListener(GeneralRefresh);
        
        foreach (var kvp in modificationContainer.ModificationsActive)
        {
            layout.AddPrefab(togglePrefab, toggle =>
            {
                InitToggle(toggle, kvp.Key, kvp.Value);
                _toggles[kvp.Key] = toggle;
            });
        }
        
        GeneralRefresh();
    }
    
    private void InitToggle(CallbackToggle toggle, AssetRefContainer<ModificationSO> modRef, bool active)
    {
        toggle.Init(new CallbackToggleData(HandleToggle, modRef, active));
        ContextualUIObjectBuilder.BuildObject(toggle.gameObject, toggleContextualData, modRef.Asset.Data.Context);
    }

    private void HandleToggle(object callbackObj, bool value)
    {
        if (callbackObj is not AssetRefContainer<ModificationSO> modificationRef) return;
        if (value) HandleActivatePressed(modificationRef);
        else HandleDeactivatePressed(modificationRef);
    }

    private void HandleActivatePressed(AssetRefContainer<ModificationSO> modRef)
    {
        var failureInfo = new RestrictionFailureInfo();
        if (!_modificationContainer.TryActivateModification(modRef, failureInfo))
        {
            RestrictionFailureDisplay.Instance.DisplayFailure(failureInfo);
        }
        InitToggle(_toggles[modRef], modRef, _modificationContainer.ModificationsActive[modRef]);
    }

    private void HandleDeactivatePressed(AssetRefContainer<ModificationSO> modRef)
    {
        _modificationContainer.TryDeactivateModification(modRef);
        InitToggle(_toggles[modRef], modRef, _modificationContainer.ModificationsActive[modRef]);
    }

    private void GeneralRefresh()
    {
        foreach (var kvp in _toggles)
        {
            var restrictionInfo = ModificationHelpers.GenerateRestrictionInfo(_modificationContainer.ModifiedPlaceable, kvp.Key.Asset.Data);
            var failureInfo = new RestrictionFailureInfo();
            if (RestrictionHelper.CheckRestrictions(kvp.Key.Asset.Data.ActivationRestrictions, restrictionInfo, failureInfo))
            {
                kvp.Value.Toggle.RemoveOneComponent<RestrictionUIBlocker>();
                continue;
            }
            
            var blocker = kvp.Value.Toggle.AddOrGetComponent<RestrictionUIBlocker>();
            blocker.Init(failureInfo.FailureType, restrictedBlockingPrefab);
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

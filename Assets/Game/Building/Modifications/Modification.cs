using System.Collections.Generic;
using UnityEngine;

public class Modification : MonoBehaviour
{
    [SerializeField] private List<ModificationComponent> componentPrefabs;

    private ModificationData _modData;
    private Building _building;
    private Transform _mainTransform;
    private List<ModificationComponent> _activeComponents = new();

    public void Init(ModificationData data, Building building, Transform mainTransform)
    {
        _modData = data;
        _building = building;
        _mainTransform = mainTransform;
    }

    public bool TryActivate(RestrictionFailureInfo failureInfo)
    {
        var restrictionInfo = ModificationHelpers.GenerateRestrictionInfo(_building, _modData);
        if (!RestrictionHelper.TryPassRestrictions(_modData.ActivationRestrictions, restrictionInfo, failureInfo)) return false;
        ForceActivate();
        return true;
    }

    public void ForceActivate()
    {
        Deactivate();
        foreach (var prefab in componentPrefabs)
        {
            var component = Instantiate(prefab, transform);
            component.Activate(_mainTransform);
            _activeComponents.Add(component);
        }
    }

    public bool TryDeactivate()
    {
        var restrictionInfo = ModificationHelpers.GenerateRestrictionInfo(_building, _modData);
        if (!RestrictionHelper.TryPassRestrictions(_modData.SaleRestrictions, restrictionInfo, new RestrictionFailureInfo())) return false;
        Deactivate();
        return true;
    }

    private void Deactivate()
    {
        foreach (var component in _activeComponents)
        {
            component.Deactivate();
            Destroy(component.gameObject);
        }
        _activeComponents.Clear();
    }

    public bool CheckRestrictions(RestrictionFailureInfo failureInfo)
    {
        var restrictionInfo = ModificationHelpers.GenerateRestrictionInfo(_building, _modData);
        return RestrictionHelper.CheckRestrictions(_modData.ActivationRestrictions, restrictionInfo, failureInfo);
    }
}
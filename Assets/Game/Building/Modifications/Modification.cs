using System.Collections.Generic;
using UnityEngine;

public class Modification : MonoBehaviour
{
    [SerializeField] private List<ModificationComponent> componentPrefabs;

    private ModificationInfo _modInfo;
    private BuildingRestrictionInfo _restrictionInfo;
    private Transform _mainTransform;
    private List<ModificationComponent> _activeComponents = new();

    public void Init(ModificationInfo info, Transform mainTransform)
    {
        _modInfo = info;
        _mainTransform = mainTransform;
    }

    public bool TryActivate(Building building, RestrictionFailureInfo failureInfo)
    {
        _restrictionInfo = ModificationHelpers.GenerateRestrictionInfo(building, _modInfo);
        if (!RestrictionHelper.TryPassRestrictions(_modInfo.ActivationRestrictions, _restrictionInfo, failureInfo)) return false;
        Activate();
        return true;
    }

    private void Activate()
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
        if (!RestrictionHelper.TryPassRestrictions(_modInfo.SaleRestrictions, _restrictionInfo, new RestrictionFailureInfo())) return false;
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
}
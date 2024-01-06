using System.Collections.Generic;
using UnityEngine;

public class Modification : MonoBehaviour
{
    [SerializeField] private List<ModificationComponent> componentPrefabs;

    private ModificationData _modData;
    private Placeable _placeable;
    private Transform _mainTransform;
    private List<ModificationComponent> _activeComponents = new();

    public void Init(ModificationData data, Placeable placeable, Transform mainTransform)
    {
        _modData = data;
        _placeable = placeable;
        _mainTransform = mainTransform;
    }

    public bool TryActivate(RestrictionFailureInfo failureInfo)
    {
        var restrictionInfo = ModificationHelpers.GenerateRestrictionInfo(_placeable, _modData);
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

    public void Deactivate()
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
        var restrictionInfo = ModificationHelpers.GenerateRestrictionInfo(_placeable, _modData);
        return RestrictionHelper.CheckRestrictions(_modData.ActivationRestrictions, restrictionInfo, failureInfo);
    }
}
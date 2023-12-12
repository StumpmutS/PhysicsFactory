﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModificationContainer : MonoBehaviour
{
    [SerializeField] private Transform transformContainer;
    [SerializeField] private Building modifiedBuilding;
    public Building ModifiedBuilding => modifiedBuilding;
    [SerializeField] private List<ModificationData> modifications;
    public Dictionary<ModificationData, bool> ModificationsActive =>
        modifications.ToDictionary(m => m, m => _activeModifications.Keys.Contains(m));

    private Dictionary<ModificationData, Modification> _activeModifications = new();

    public bool TryActivateModification(ModificationData modificationData, RestrictionFailureInfo failureInfo)
    {
        var modification = Instantiate(modificationData.Info.ModificationPrefab, transformContainer);
        if (!_activeModifications.TryAdd(modificationData, modification))
        {
            Destroy(modification.gameObject);
            return false;
        }
        modification.Init(modificationData.Info, transform);
        if (modification.TryActivate(modifiedBuilding, failureInfo)) return true;
        
        _activeModifications.Remove(modificationData);
        Destroy(modification.gameObject);
        return false;
    }

    public bool TryDeactivateModification(ModificationData modificationData)
    {
        if (!_activeModifications.TryGetValue(modificationData, out var modification)) return false;
        if (!modification.TryDeactivate()) return false;
        
        _activeModifications.Remove(modificationData);
        Destroy(modification.gameObject);
        return true;
    }

    public void CheckModifications()
    {
        var copy = new Dictionary<ModificationData, Modification>(_activeModifications);
        foreach (var kvp in copy)
        {
            if (kvp.Value.CheckRestrictions(modifiedBuilding, new RestrictionFailureInfo())) continue;
            TryDeactivateModification(kvp.Key);
        }
    }
    
    public void HandleBuildingSold()
    {
        foreach (var modificationData in modifications)
        {
            TryDeactivateModification(modificationData);
        }
    }
}
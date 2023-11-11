using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModificationContainer : MonoBehaviour
{
    [SerializeField] private Transform transformContainer;
    [SerializeField] private Building modifiedBuilding;
    public Building ModifiedBuilding => modifiedBuilding;
    [SerializeField] private List<ModificationData> modifications;
    public Dictionary<ModificationData, bool> Modifications =>
        modifications.ToDictionary(m => m, m => _activeModifications.Keys.Contains(m));

    private Dictionary<ModificationData, Modification> _activeModifications = new();

    public bool TryActivateModification(ModificationData modificationData)
    {
        var modification = Instantiate(modificationData.Info.ModificationPrefab, transformContainer);
        if (!_activeModifications.TryAdd(modificationData, modification))
        {
            Destroy(modification.gameObject);
            return false;
        }
        modification.Init(modificationData.Info, transform);
        if (modification.TryActivate(modifiedBuilding)) return true;
        _activeModifications.Remove(modificationData);
        Destroy(modification.gameObject);
        return false;
    }

    public bool TrySellModification(ModificationData modificationData)
    {
        if (!_activeModifications.TryGetValue(modificationData, out var modification)) return false;
        if (!modification.TryDeactivate()) return false;
        
        _activeModifications.Remove(modificationData);
        Destroy(modification.gameObject);
        return true;
    }

    public void HandleBuildingSold()
    {
        foreach (var modificationData in modifications)
        {
            TrySellModification(modificationData);
        }
    }
}
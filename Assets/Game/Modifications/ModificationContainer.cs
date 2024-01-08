using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class ModificationContainer : MonoBehaviour, ISaveable<SaveableObjectSaveData>, ILoadable<ModificationSaveData>
{
    [SerializeField] private Transform transformContainer;
    [FormerlySerializedAs("modifiedBuilding")] [SerializeField] private Placeable modifiedPlaceable;
    public Placeable ModifiedPlaceable => modifiedPlaceable;
    [SerializeField] private List<AssetRefContainer<ModificationSO>> modifications;

    public Dictionary<AssetRefContainer<ModificationSO>, bool> ModificationsActive
    {
        get
        {
            var result = _activeModifications.ToDictionary(kvp => kvp.Key, kvp => true);

            foreach (var modificationRef in modifications)
            {
                bool contains = false;
                
                foreach (var kvp in result)
                {
                    if (kvp.Key.Asset == modificationRef.Asset) contains = true;
                }

                if (!contains) result.Add(modificationRef, false);
            }

            return result;
        }
    }
    
    private Dictionary<AssetRefContainer<ModificationSO>, Modification> _activeModifications = new();

    public bool TryActivateModification(AssetRefContainer<ModificationSO> modificationRef, RestrictionFailureInfo failureInfo)
    {
        if (!TryInitModification(modificationRef, modifiedPlaceable, out var modification)) return false;
        if (modification.TryActivate(failureInfo)) return true;
        
        _activeModifications.Remove(modificationRef);
        Destroy(modification.gameObject);
        return false;
    }

    private bool TryInitModification(AssetRefContainer<ModificationSO> modificationRef, Placeable placeable, out Modification modification)
    {
        modification = Instantiate(modificationRef.Asset.Data.ModificationPrefab, transformContainer);
        if (!_activeModifications.TryAdd(modificationRef, modification))
        {
            Destroy(modification.gameObject);
            return false;
        }
        modification.Init(modificationRef.Asset.Data, placeable, transform);
        return true;
    }
    
    public bool TryDeactivateModification(AssetRefContainer<ModificationSO> modificationRef)
    {
        if (!_activeModifications.TryGetValue(modificationRef, out var modification)) return false;
        
        modification.Deactivate();
        _activeModifications.Remove(modificationRef);
        Destroy(modification.gameObject);
        return true;
    }

    public void CheckModifications()
    {
        var copy = new Dictionary<AssetRefContainer<ModificationSO>, Modification>(_activeModifications);
        foreach (var kvp in copy)
        {
            if (kvp.Value.CheckRestrictions(new RestrictionFailureInfo())) continue;
            TryDeactivateModification(kvp.Key);
        }
    }

    public void Save(SaveableObjectSaveData data, AssetRefCollection assetRefCollection)
    {
        data.ModificationSaveData ??= new ModificationSaveData();
        data.ModificationSaveData.ActiveModificationIndexes =
            _activeModifications.Keys.Select(r => assetRefCollection.Add(r.Reference)).ToList();
    }

    public LoadingInfo Load(ModificationSaveData data, AssetRefCollection assetRefCollection)
    {
        foreach (var index in data.ActiveModificationIndexes)
        {
            var assetRef = assetRefCollection.GetContainerized<ModificationSO>(index);
            
            //Prefer using pre existing ref container
            foreach (var modificationRef in modifications)
            {
                if (assetRef.Asset != modificationRef.Asset) continue;
                
                assetRef = modificationRef;
                break;
            }
            
            ForceActivateModification(assetRef);
        }

        return LoadingInfo.Completed(_activeModifications, ELoadCompletionStatus.Succeeded);
    }

    private void ForceActivateModification(AssetRefContainer<ModificationSO> modificationRef)
    {
        if (!TryInitModification(modificationRef, modifiedPlaceable, out var modification)) return;
        modification.ForceActivate();
    }
}
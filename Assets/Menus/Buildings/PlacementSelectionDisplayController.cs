using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlacementSelectionDisplayController : DataSelectionDisplayController<PlacementDisplayData>
{
    [SerializeField] private PlacementPersistenceHandler placementPersistenceHandler;
    
    private HashSet<AssetRefContainer<PlacementSO>> _placementContainers = new();

    [FormerlySerializedAs("OnBuildingsChanged")] public UnityEvent<IEnumerable<AssetRefContainer<PlacementSO>>> OnPlacementContainersChanged = new();

    protected override bool DataSelected(PlacementDisplayData data)
    {
        return placementPersistenceHandler.PlacementContainers.Any(c => c.Asset == data.PlacementContainer.Asset);
    }

    protected override void HandleToggle(object data, bool value)
    {
        if (data is not PlacementDisplayData displayData) return;

        if (value) _placementContainers.Add(displayData.PlacementContainer);
        else _placementContainers.Remove(displayData.PlacementContainer);
        
        OnPlacementContainersChanged.Invoke(_placementContainers);
    }
}
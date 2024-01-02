using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BuildingSelectionDisplayController : DataSelectionDisplayController<PlaceableBuildingDisplayData>
{
    [SerializeField] private PlacementPersistenceHandler placementPersistenceHandler;
    
    private HashSet<AssetRefContainer<BuildingPlacementSO>> _buildings = new();

    public UnityEvent<IEnumerable<AssetRefContainer<BuildingPlacementSO>>> OnBuildingsChanged = new();

    protected override bool DataSelected(PlaceableBuildingDisplayData data)
    {
        return placementPersistenceHandler.BuildingContainers.Any(c => c.Asset == data.BuildingContainer.Asset);
    }

    protected override void HandleToggle(object data, bool value)
    {
        if (data is not PlaceableBuildingDisplayData displayData) return;

        if (value) _buildings.Add(displayData.BuildingContainer);
        else _buildings.Remove(displayData.BuildingContainer);
        
        OnBuildingsChanged.Invoke(_buildings);
    }
}
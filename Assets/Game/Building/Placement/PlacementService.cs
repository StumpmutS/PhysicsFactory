using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlacementService : DataService<BuildingPlacementData>
{
    [SerializeField] private List<BuildingPlacementSO> editModeBuildings;
    [SerializeField] private PlacementPersistenceHandler persistenceHandler;

    private bool _editing;

    public UnityEvent OnBuildingsReady = new();

    public void HandleBuildingsLoaded()
    {
        OnBuildingsReady.Invoke();
    }
    
    public void HandleEditMode()
    {
        _editing = true;
        OnBuildingsReady.Invoke();
    }
    
    public override IEnumerable<BuildingPlacementData> RequestData()
    {
        return _editing
            ? editModeBuildings.Select(s => s.Data)
            : persistenceHandler.BuildingContainers.Select(arc => arc.Asset.Data);
    }
}
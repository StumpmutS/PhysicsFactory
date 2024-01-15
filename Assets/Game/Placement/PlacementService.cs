using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlacementService : DataService<IEnumerable<PlacementData>>
{
    [FormerlySerializedAs("editModeBuildings")] [SerializeField] private List<PlacementSO> editModePlacementSos;
    [SerializeField] private PlacementPersistenceHandler persistenceHandler;

    private bool _editing;

    [FormerlySerializedAs("OnPlaceablesReady")] [FormerlySerializedAs("OnBuildingsReady")] public UnityEvent OnPlacementReady = new();

    public void HandlePlacementLoaded()
    {
        OnPlacementReady.Invoke();
    }
    
    public void HandleEditMode()
    {
        _editing = true;
        OnPlacementReady.Invoke();
    }
    
    public override IEnumerable<PlacementData> RequestData()
    {
        return _editing
            ? editModePlacementSos.Select(s => s.Data)
            : persistenceHandler.PlacementContainers.Select(arc => arc.Asset.Data);
    }
}
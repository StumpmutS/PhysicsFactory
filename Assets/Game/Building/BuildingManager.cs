using System.Collections.Generic;
using UnityEngine.Events;
using Utility.Scripts;

public class BuildingManager : Singleton<BuildingManager>
{
    private HashSet<Building> _buildings = new();

    public UnityEvent OnBuildingAdded;

    public void AddBuilding(Building building)
    {
        if (!_buildings.Add(building)) return;
        
        OnBuildingAdded.Invoke();
    }

    public void RemoveBuilding(Building building)
    {
        _buildings.Remove(building);
    }
}
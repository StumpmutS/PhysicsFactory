using System.Collections.Generic;
using UnityEngine;

public class LevelPlaceableBuildingService : DataService<PlaceableBuildingDisplayData>
{
    [SerializeField] private List<PlaceableBuildingDisplayData> buildings;

    public override IEnumerable<PlaceableBuildingDisplayData> RequestData()
    {
        return buildings;
    }
}
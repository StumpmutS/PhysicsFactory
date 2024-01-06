using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelPlacementService : DataService<PlacementDisplayData>
{
    [FormerlySerializedAs("buildings")] [SerializeField] private List<PlacementDisplayData> placementData;

    public override IEnumerable<PlacementDisplayData> RequestData()
    {
        return placementData;
    }
}
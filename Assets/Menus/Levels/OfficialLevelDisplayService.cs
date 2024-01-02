using System.Collections.Generic;
using UnityEngine;

public class OfficialLevelDisplayService : DataService<LevelDisplayData>
{
    public override IEnumerable<LevelDisplayData> RequestData()
    {
        Debug.LogWarning("Official level service not implemented");
        return new List<LevelDisplayData>();
    }
}
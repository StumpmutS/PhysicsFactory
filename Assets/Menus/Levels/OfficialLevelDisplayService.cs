using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OfficialLevelDisplayService : DataService<LevelDisplayData>
{
    [SerializeField] private List<LevelSO> levels;

    public override IEnumerable<LevelDisplayData> RequestData()
    {
        return levels.Select(so => new LevelDisplayData(so.LevelData));
    }
}
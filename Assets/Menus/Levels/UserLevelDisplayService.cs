using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UserLevelDisplayService : DataService<IEnumerable<LevelDisplayData>>
{
    [SerializeField] protected LocalPathSO pathSo;
    
    public override IEnumerable<LevelDisplayData> RequestData()
    {
        return LocalDataPersistenceHandler.GetPathJsonData<LevelData>(pathSo.LocalPathData).Select(l => new LevelDisplayData(l));
    }
}
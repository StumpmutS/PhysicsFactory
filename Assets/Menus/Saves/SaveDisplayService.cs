using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveDisplayService : DataService<IEnumerable<SaveDisplayData>>
{
    [SerializeField] protected LocalPathSO pathSo;

    public override IEnumerable<SaveDisplayData> RequestData()
    {
        return LocalDataPersistenceHandler.GetPathJsonData<SaveData>(pathSo.LocalPathData).Select(s => new SaveDisplayData(s));
    }
}
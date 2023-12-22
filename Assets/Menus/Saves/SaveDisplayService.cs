using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveDisplayService : MonoBehaviour
{
    [SerializeField] private LocalSavePathSO pathSo;

    public IEnumerable<SaveDisplayData> RequestData()
    {
        return LocalDataPersistenceHandler.GetSaves(pathSo.LocalSavePathInfo).Select(s => new SaveDisplayData(s));
    }
}
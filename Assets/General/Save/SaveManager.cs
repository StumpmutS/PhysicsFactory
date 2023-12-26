using UnityEngine;
using UnityEngine.Serialization;
using Utility.Scripts;
using Utility.Scripts.Extensions;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private LocalSavePathSO localSavePathSo;
    
    public void Save(SaveInfo info)
    {
        var assetRefCollection = new AssetRefCollection();
        var saveData = new SaveData(info, new LevelData(assetRefCollection));

        SaveHelpers.GroupSave(SaveHelpers.GetSaveables<SaveData>(), saveData, assetRefCollection);

        LocalDataPersistenceHandler.SaveTo(saveData, localSavePathSo.LocalSavePathInfo);
    }
}
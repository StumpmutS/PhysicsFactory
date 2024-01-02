using UnityEngine;
using UnityEngine.Serialization;
using Utility.Scripts;
using Utility.Scripts.Extensions;

public class SessionSaveManager : Singleton<SessionSaveManager>
{
    [FormerlySerializedAs("localSavePathSo")] [SerializeField] private LocalPathSO localPathSo;
    
    public void Save(SaveInfo info, LevelInfo levelInfo)
    {
        var assetRefCollection = new AssetRefCollection();
        var saveData = new SaveData(info, new LevelData(levelInfo, assetRefCollection));

        SaveHelpers.GroupSave(SaveHelpers.GetSaveables<SaveData>(), saveData, assetRefCollection);

        LocalDataPersistenceHandler.SaveTo(saveData, saveData.SaveInfo.Id, localPathSo.LocalPathData);
    }
}
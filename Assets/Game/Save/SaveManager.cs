using UnityEngine;
using UnityEngine.Serialization;
using Utility.Scripts;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private LocalSavePathData localSavePathData;
    
    public void Save(SaveInfo info)
    {
        var saveData = new SaveData(info);

        SaveHelpers.GroupSave(SaveHelpers.GetSaveables<SaveData>(), saveData);

        LocalDataPersistenceHandler.SaveTo(saveData, localSavePathData.LocalSavePathInfo.SaveDirectoryPath,
            localSavePathData.LocalSavePathInfo.DefaultLocalSaveFileName);
    }
}
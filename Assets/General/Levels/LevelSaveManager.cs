using System;
using UnityEngine;
using Utility.Scripts;

public class LevelSaveManager : Singleton<LevelSaveManager>
{
    [SerializeField] private LocalPathSO localPathSo;
    
    public void SaveLevel(LevelInfo levelInfo)
    {
        var assetRefCollection = new AssetRefCollection();
        var levelData = new LevelData(levelInfo, assetRefCollection);

        SaveHelpers.GroupSave(SaveHelpers.GetSaveables<LevelData>(), levelData, assetRefCollection);

        LocalDataPersistenceHandler.SaveTo(levelData, levelData.LevelInfo.Id, localPathSo.LocalPathData);
    }
}
using System;
using UnityEngine;
using Utility.Scripts;

public class LevelSaveManager : Singleton<LevelSaveManager>
{
    [SerializeField] private LocalPathSO localPathSo;
    
    public void SaveLevel(LevelInfo levelInfo)
    {
        var levelData = LevelSaveHelpers.GetCurrentLevelData(levelInfo);
        LocalDataPersistenceHandler.SaveTo(levelData, levelData.LevelInfo.Id, localPathSo.LocalPathData);
    }
}
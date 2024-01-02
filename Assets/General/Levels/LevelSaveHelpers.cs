using System;

public static class LevelSaveHelpers
{
    public static LevelInfo GetUpdatedLevelInfo()
    {
        var timestamp = new SerializableDateTime(DateTime.Now);
        if (LevelDataHandler.Instance == null || LevelDataHandler.Instance.Info == null) return new LevelInfo("", timestamp);
        
        var currentInfo = LevelDataHandler.Instance.Info;
        return new LevelInfo(currentInfo.Name, timestamp, currentInfo.SerializableGuid.Guid);
    }

    public static LevelData GetCurrentLevelData(LevelInfo levelInfo = null)
    {
        levelInfo ??= GetUpdatedLevelInfo();
        
        var assetRefCollection = new AssetRefCollection();
        var levelData = new LevelData(levelInfo, assetRefCollection);

        SaveHelpers.GroupSave(SaveHelpers.GetSaveables<LevelData>(), levelData, assetRefCollection);

        return levelData;
    }
}
using System;
using System.Linq;
using UnityEngine;
using Utility.Scripts;

public class LevelManager : Singleton<LevelManager>, ISaveable<SaveData>, ILoadable<SaveData>
{
    public string Name { get; private set; }

    public event Action<ILoadable<SaveData>> OnLoadComplete = delegate { };
    
    public void Save(SaveData data)
    {
        data.LevelData.LevelInfo.Name = "TestLevel";
        
        SaveHelpers.GroupSave(SaveHelpers.GetSaveables<LevelData>(), data.LevelData);
    }

    public void Load(SaveData data)
    {
        Name = data.LevelData.LevelInfo.Name;
        
        var loadables = SaveHelpers.GetLoadables<LevelData>();
        var loader = new SaveHelpers.Loader<LevelData>(loadables, data.LevelData);
        loader.OnComplete += HandleLoadComplete;
        loader.Load();
    }

    private void HandleLoadComplete(SaveHelpers.Loader<LevelData> loader)
    {
        loader.OnComplete -= HandleLoadComplete;
        OnLoadComplete.Invoke(this);
    }
}
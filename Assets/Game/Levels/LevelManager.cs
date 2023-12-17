using System;
using System.Linq;
using UnityEngine;
using Utility.Scripts;

public class LevelManager : Singleton<LevelManager>, ISaveable<SaveData>, ILoadable<SaveData>
{
    public string Name { get; private set; }
    
    public void Save(SaveData data)
    {
        data.LevelData.LevelInfo.Name = "TestLevel";
        
        SaveHelpers.GroupSave(SaveHelpers.GetSaveables<LevelData>(), data.LevelData);
    }

    public LoadingInfo Load(SaveData data)
    {
        Name = data.LevelData.LevelInfo.Name;
        
        var loadables = SaveHelpers.GetLoadables<LevelData>();
        var loader = new UnorderedLoader(loadables.Select(l => new LoadableData(() => l.Load(data.LevelData))));
        return loader.Load();
    }
}
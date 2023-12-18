using System;
using System.Linq;
using Utility.Scripts;

public class LevelManager : Singleton<LevelManager>, ISaveable<SaveData>, ILoadable<SaveData>
{
    public string Name { get; private set; }

    private void Start()
    {
        if (LevelDataContainer.Instance != null && LevelDataContainer.Instance.LevelData != null)
        {
            Load(LevelDataContainer.Instance.LevelData);
        }
    }

    public void Save(SaveData data)
    {
        data.LevelData.LevelInfo.Name = "TestLevel";
        
        SaveHelpers.GroupSave(SaveHelpers.GetSaveables<LevelData>(), data.LevelData);
    }

    public LoadingInfo Load(SaveData data) => Load(data.LevelData);

    private LoadingInfo Load(LevelData data)
    {
        Name = data.LevelInfo.Name;
        
        var loadables = SaveHelpers.GetLoadables<LevelData>();
        var loader = new UnorderedLoader(loadables.Select(l => new LoadableData(() => l.Load(data))));
        return loader.Load();
    }
}
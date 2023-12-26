using System.Linq;
using UnityEngine;
using Utility.Scripts;

public class LevelDataHandler : Singleton<LevelDataHandler>, ISaveable<SaveData>
{
    public string Name { get; private set; }
    public bool Loaded { get; private set; }

    private void Start()
    {
        if (LevelDataContainer.Instance != null && LevelDataContainer.Instance.LevelData != null)
        {
            Load(LevelDataContainer.Instance.LevelData, LevelDataContainer.Instance.LevelData.AssetRefCollection);
        }
    }

    public void Save(SaveData data, AssetRefCollection assetRefCollection)
    {
        data.LevelData.LevelInfo.Name = "TestLevel";
        
        SaveHelpers.GroupSave(SaveHelpers.GetSaveables<LevelData>(), data.LevelData, assetRefCollection);
    }

    private void Load(LevelData data, AssetRefCollection assetRefCollection)
    {
        Name = data.LevelInfo.Name;
        Loaded = false;
        
        var loadables = SaveHelpers.GetLoadables<LevelData>();
        var levelDataLoader = new UnorderedLoader(loadables
            .Select(l => new LoadableData(() => l.Load(data, assetRefCollection))));

        var combinedLoader = new OrderedLoader(new[]
        {
            new LoadableData(assetRefCollection.LoadAll),
            new LoadableData(levelDataLoader.Load)
        });
        
        var info = combinedLoader.Load();
        info.OnComplete += HandleLoadComplete;
    }

    private void HandleLoadComplete(LoadingInfo info)
    {
        Loaded = true;
        
        if (info.Status == ELoadCompletionStatus.Failed)
        {
            Debug.LogException(info.Exception);
            return;
        }
        
        Debug.Log("Successfully loaded");
    }
}
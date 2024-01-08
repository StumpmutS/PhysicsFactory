using System;
using System.Linq;
using Utility.Scripts;

public class SaveableObjectLoadManager : Singleton<SaveableObjectLoadManager>, ILoadable<LevelData>
{
    private InitializedSaveableObjectData _initializedObjectData;

    public LoadingInfo Load(LevelData data, AssetRefCollection assetRefCollection)
    {
        _initializedObjectData = new InitializedSaveableObjectData();

        foreach (var saveableObjectSaveData in data.SaveableObjectSaveData)
        {
            var asset = LevelLoadingHelpers.GetSaveableAsset(data, saveableObjectSaveData, assetRefCollection);
            if (asset == default)
            {
                var exception = new Exception("Problem retrieving asset from collection, see above error logs");
                return LoadingInfo.Completed(this, ELoadCompletionStatus.Failed, exception);
            }
            
            _initializedObjectData.SaveableObjects.Add(Instantiate(asset), saveableObjectSaveData);
        }
        
        return HandleObjectsInitialized(assetRefCollection);
    }

    private LoadingInfo HandleObjectsInitialized(AssetRefCollection assetRefCollection)
    {
        //Create loaders on demand so that GetComponents is called after each phase in case a relevant component is added in a previous phase
        UnorderedLoader CreateLoader<TData>(Func<SaveableObjectSaveData, TData> dataSelector)
        {
            return new UnorderedLoader(_initializedObjectData.SaveableObjects.SelectMany(kvp =>
                kvp.Key.GetComponentsInChildren<ILoadable<TData>>()
                    .Select(l => new LoadableData(() =>
                        l.Load(dataSelector.Invoke(kvp.Value), assetRefCollection)))));
        }
        
        var phaseLoader = new OrderedLoader(new []
        {
            new LoadableData(() => CreateLoader(d => d).Load()),
            new LoadableData(() => CreateLoader(d => d.PlacedSaveData).Load()),
            new LoadableData(() => CreateLoader(d => d.ExtractorSaveData).Load()),
            new LoadableData(() => CreateLoader(d => d.UpgradeSaveData).Load()),
            new LoadableData(() => CreateLoader(d => d.ModificationSaveData).Load()),
            new LoadableData(() => CreateLoader(d => d.GeneratorSaveData).Load()),
            new LoadableData(() => CreateLoader(d => d.CurrentSaveData).Load()),
            new LoadableData(() => CreateLoader(d => d.EnergySpreadSaveData).Load()),
            new LoadableData(() => CreateLoader(d => d).Load()) //final pass for any new components
        });

        return phaseLoader.Load();
    }
}

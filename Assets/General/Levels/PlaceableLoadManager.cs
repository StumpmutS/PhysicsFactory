using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Scripts;

public class PlaceableLoadManager : Singleton<PlaceableLoadManager>, ILoadable<LevelData>
{
    [SerializeField] private LevelOptionsSO editorOptionsSo;
    
    private LoadingInfo _loadingInfo;
    private Dictionary<GameObject, PlaceableSaveData> _initializedTransmitters = new();
    private LoadingInfo _phaseInfo;

    public LoadingInfo Load(LevelData data, AssetRefCollection assetRefCollection)
    {
        _initializedTransmitters.Clear();
        _phaseInfo = null;
        _loadingInfo = new LoadingInfo(GetPercent);

        foreach (var placeableSaveData in data.PlaceableSaveData)
        {
            var asset = LevelLoadingHelpers.GetPlaceableAsset(editorOptionsSo, data, placeableSaveData, assetRefCollection);
            if (asset == default)
            {
                _loadingInfo.Exception = new Exception("Problem retrieving asset from collection, see above error logs");
                FinishLoad(ELoadCompletionStatus.Failed);
                break;
            }
            _initializedTransmitters.Add(Instantiate(asset), placeableSaveData);
        }
        
        HandleTransmittersInitialized(assetRefCollection);
        
        return _loadingInfo;
    }
    
    private void HandleTransmittersInitialized(AssetRefCollection assetRefCollection)
    {
        //Create loaders on demand so that GetComponents is called after each phase in case a relevant component is added in a previous phase
        UnorderedLoader CreateLoader<TData>(Func<PlaceableSaveData, TData> dataSelector)
        {
            return new UnorderedLoader(_initializedTransmitters.SelectMany(kvp =>
                kvp.Key.GetComponentsInChildren<ILoadable<TData>>()
                    .Select(l => new LoadableData(() => l.Load(dataSelector.Invoke(kvp.Value), assetRefCollection)))));
        }
        
        var phaseLoader = new OrderedLoader(new []
        {
            new LoadableData(() => CreateLoader(data => data.IdentifiableObjectSaveData).Load()),
            new LoadableData(() => CreateLoader(data => data.PlacedSaveData).Load()),
            new LoadableData(() => CreateLoader(data => data.UpgradeSaveData).Load()),
            new LoadableData(() => CreateLoader(data => data.ModificationSaveData).Load()),
            new LoadableData(() => CreateLoader(data => data.GeneratorSaveData).Load()),
            new LoadableData(() => CreateLoader(data => data.CurrentSaveData).Load()),
            new LoadableData(() => CreateLoader(data => data.EnergySpreadSaveData).Load()),
            new LoadableData(() => CreateLoader(data => data).Load()) //final pass for any new components
        });

        _phaseInfo = phaseLoader.Load();
        _phaseInfo.OnComplete += HandleLoadComplete;
    }

    private void HandleLoadComplete(LoadingInfo info)
    {
        if (info.Status != ELoadCompletionStatus.Succeeded)
        {
            _loadingInfo.Exception = info.Exception;
            FinishLoad(ELoadCompletionStatus.Failed);
            return;
        }
        
        FinishLoad(ELoadCompletionStatus.Succeeded);
    }

    private void FinishLoad(ELoadCompletionStatus status)
    {
        _loadingInfo.Status = status;
        _loadingInfo.Result = this;
        _loadingInfo.Complete();
    }

    private int GetPercent() => _phaseInfo?.Percentage ?? 0;
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using Utility.Scripts;

public class BuildingLoadManager : Singleton<BuildingLoadManager>, ILoadable<BuildingSaveData[]>
{
    [SerializeField, Range(0, 100)] private int assetRefInstantiationPercentWeight;
    
    private LoadingInfo _loadingInfo;
    private Dictionary<GameObject, BuildingSaveData> _initializedTransmitters = new();
    private LoadingInfo _transmitterInfo;
    private LoadingInfo _phaseInfo;

    public LoadingInfo Load(BuildingSaveData[] data)
    {
        _initializedTransmitters.Clear();
        _transmitterInfo = null;
        _phaseInfo = null;
        _loadingInfo = new LoadingInfo(GetPercent);

        var transmitterLoader = new UnorderedLoader(data.Select(d =>
            new LoadableData(() => LoadingInfo.From(d.BuildingPrefabReference.InstantiateAsync()),
                info => HandleRefInstantiated(info, d))));
        _transmitterInfo = transmitterLoader.Load();
        _transmitterInfo.OnComplete += HandleTransmittersInitialized;
        
        return _loadingInfo;
    }

    private void HandleRefInstantiated(LoadingInfo result, BuildingSaveData data)
    {
        if (result.Status == ELoadCompletionStatus.Failed || result.Result is not GameObject go)
        {
            Debug.LogError($"Error instantiating asset reference, result was: {result.Result}");
            return;
        }

        _initializedTransmitters.Add(go, data);
    }

    private void HandleTransmittersInitialized(LoadingInfo loadingInfo)
    {
        UnorderedLoader CreateLoader<TData>(Func<BuildingSaveData, TData> dataSelector)
        {
            return new UnorderedLoader(_initializedTransmitters.SelectMany(kvp =>
                kvp.Key.GetComponentsInChildren<ILoadable<TData>>()
                    .Select(l => new LoadableData(() => l.Load(dataSelector.Invoke(kvp.Value))))));
        }
        
        //Use methods so that GetComponents is called after each phase in case a relevant component is added in a previous phase
        UnorderedLoader PlacedBuildingLoader() => CreateLoader(data => data.PlacedBuildingSaveData);
        UnorderedLoader UpgradeLoader() => CreateLoader(data => data.UpgradeSaveData);
        UnorderedLoader ModificationLoader() => CreateLoader(data => data.ModificationSaveData);
        UnorderedLoader CurrentLoader() => CreateLoader(data => data.CurrentSaveData);
        UnorderedLoader EnergyLoader() => CreateLoader(data => data.EnergySpreadSaveData);
        UnorderedLoader GeneralBuildingLoader() => CreateLoader(data => data); //final pass for any new components

        var phaseLoader = new OrderedLoader(new []
        {
            new LoadableData(() => PlacedBuildingLoader().Load()),
            new LoadableData(() => UpgradeLoader().Load()),
            new LoadableData(() => ModificationLoader().Load()),
            new LoadableData(() => CurrentLoader().Load()),
            new LoadableData(() => EnergyLoader().Load()),
            new LoadableData(() => GeneralBuildingLoader().Load())
        });

        _phaseInfo = phaseLoader.Load();
        _phaseInfo.OnComplete += HandleLoadComplete;
    }

    private void HandleLoadComplete(LoadingInfo info)
    {
        if (info.Status != ELoadCompletionStatus.Succeeded)
        {
            _loadingInfo.Exception = info.Exception;
            _loadingInfo.Status = ELoadCompletionStatus.Failed;
        }

        _loadingInfo.Status = ELoadCompletionStatus.Succeeded;
        _loadingInfo.Result = this;
        _loadingInfo.Complete();
    }

    private int GetPercent()
    {
        int percent = 0;
        
        if (_transmitterInfo == null) return percent / 100;
        percent += _transmitterInfo.Percentage * assetRefInstantiationPercentWeight;
        
        if (_phaseInfo == null) return percent / 100;
        percent += _phaseInfo.Percentage * (100 - assetRefInstantiationPercentWeight);

        return percent / 100;
    }
}

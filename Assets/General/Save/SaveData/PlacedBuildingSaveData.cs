using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class PlacedBuildingSaveData
{
    [SerializeField] private PlacedBuildingData data;

    private LoadingInfo _loadingInfo;

    public PlacedBuildingSaveData(PlacedBuildingData data)
    {
        this.data = data;
    }
    
    /// <summary>
    /// Loading result is <see cref="PlacedBuildingData"/>
    /// </summary>
    public LoadingInfo LoadGameReadyData()
    {
        var info = new OrderedLoader(data.SaleRestrictionRefs.Select(r =>
            new LoadableData(() => LoadingInfo.From(AssetRefHelpers.LoadOrGet<Restriction<BuildingRestrictionInfo>>(r.Reference)),
                info => AssetRefHelpers.HandleRefContainerLoaded(info, r)))).Load();
        
        _loadingInfo = new LoadingInfo(() => info.Percentage);
        info.OnComplete += HandleLoadComplete;
        
        return _loadingInfo;
    }

    private void HandleLoadComplete(LoadingInfo info)
    {
        _loadingInfo.Result = data;
        _loadingInfo.Status = info.Status;
        _loadingInfo.Exception = info.Exception;
        _loadingInfo.Complete();
    }
}
using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class PlacedBuildingSaveData
{
    [SerializeField] private PlacedBuildingData data;

    private LoadingInfo _loadingInfo;

    public LoadingInfo LoadGameReadyData()
    {
        var info = new OrderedLoader(data.SaleRestrictionRefs.Select(r =>
            new LoadableData(() => LoadingInfo.From(r.Reference.LoadAssetAsync<Restriction<BuildingRestrictionInfo>>()),
                info => HandleRefLoaded(info, r)))).Load();
        
        _loadingInfo = new LoadingInfo(() => info.Percentage);
        info.OnComplete += HandleLoadComplete;
        
        return _loadingInfo;
    }

    private void HandleRefLoaded(LoadingInfo info, AssetRefContainer<Restriction<BuildingRestrictionInfo>> refContainer)
    {
        if (info.Status == ELoadCompletionStatus.Failed || info.Result is not Restriction<BuildingRestrictionInfo> restriction)
        {
            Debug.LogError($"Error loading asset reference, result was: {info.Result}");
            refContainer.Data = null;
            return;
        }

        refContainer.Data = restriction;
    }

    private void HandleLoadComplete(LoadingInfo info)
    {
        _loadingInfo.Result = data;
        _loadingInfo.Status = info.Status;
        _loadingInfo.Exception = info.Exception;
        _loadingInfo.Complete();
    }
}
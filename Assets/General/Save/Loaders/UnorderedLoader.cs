using System;
using System.Collections.Generic;
using System.Linq;

public class UnorderedLoader : Loader
{
    private List<LoadingInfo> _loadingLoadables = new();
    
    public UnorderedLoader(IEnumerable<LoadableData> data) : base(data) { }

    protected override void PreLoad()
    {
        base.PreLoad();
        _loadingLoadables.Clear();
    }

    protected override void PerformLoad()
    {
        _loadingLoadables = _loadableData.Select(data =>
        {
            var info = data.Loadable.Invoke();
            info.OnComplete += i => HandleLoadComplete(i, data.Callback);
            return info;
        }).ToList();
    }

    private void HandleLoadComplete(LoadingInfo loadingInfo, Action<LoadingInfo> callback)
    {
        callback?.Invoke(loadingInfo);

        if (loadingInfo.Status == ELoadCompletionStatus.Failed)
        {
            _loadingInfo.Exception = loadingInfo.Exception;
            FinishLoad(ELoadCompletionStatus.Failed);
        }
        
        if (_loadingLoadables.All(info => info.Status == ELoadCompletionStatus.Succeeded)) FinishLoad(ELoadCompletionStatus.Succeeded);
    }

    protected override int GetPercent()
    {
        if (_loadingLoadables.Count == 0) return 0;
        return _loadingLoadables.Sum(info => info.Percentage) / _loadingLoadables.Count;
    }
}
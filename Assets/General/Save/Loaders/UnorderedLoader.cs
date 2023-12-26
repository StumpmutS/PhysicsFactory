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
        if (_loadableData.Count == 0)
        {
            FinishLoad(ELoadCompletionStatus.Succeeded);
            return;
        }
        _loadingLoadables = _loadableData.Select(d => d.Loadable.Invoke()).ToList();
        for (int i = 0; i < _loadingLoadables.Count; i++)
        {
            var info = _loadingLoadables[i];
            var callback = _loadableData[i].Callback;
            info.OnComplete += li => HandleLoadComplete(li, callback);
        }
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
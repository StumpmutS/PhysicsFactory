using System;
using System.Collections.Generic;

public class OrderedLoader : Loader
{
    private LoadingInfo _loadingLoadable;
    private int _loadingIndex;
    
    public OrderedLoader(IEnumerable<LoadableData> data) : base(data) { }

    protected override void PreLoad()
    {
        base.PreLoad();
        _loadingIndex = 0;
        _loadingLoadable = null;
    }

    protected override void PerformLoad()
    {
        LoadNextOrFinish();
    }

    private void LoadNextOrFinish()
    {
        if (_loadingIndex >= _loadableData.Count)
        {
            FinishLoad(ELoadCompletionStatus.Succeeded);
            return;
        }

        var data = _loadableData[_loadingIndex];
        _loadingLoadable = data.Loadable.Invoke();
        _loadingLoadable.OnComplete += info => HandleLoadComplete(info, data.Callback);
    }

    private void HandleLoadComplete(LoadingInfo info, Action<LoadingInfo> callback)
    {
        callback?.Invoke(info);
        
        if (info.Status == ELoadCompletionStatus.Failed)
        {
            _loadingInfo.Exception = info.Exception;
            FinishLoad(ELoadCompletionStatus.Failed);
        }

        _loadingIndex++;
        LoadNextOrFinish();
    }

    protected override int GetPercent()
    {
        if (_loadableData.Count == 0 || _loadingLoadable == null) return 0;
        return (_loadingIndex * 100 + _loadingLoadable.Percentage) / _loadableData.Count;
    }
}
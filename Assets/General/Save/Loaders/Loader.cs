using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Loader
{
    protected List<LoadableData> _loadableData;
    protected LoadingInfo _loadingInfo;
    private bool _isLoading;

    protected Loader(IEnumerable<LoadableData> data)
    {
        _loadableData = data.ToList();
    }

    public LoadingInfo Load()
    {
        if (_isLoading) return _loadingInfo;
        _isLoading = true;

        try
        {
            PreLoad();
            PerformLoad();
        }
        catch (Exception e)
        {
            _loadingInfo.Exception = e;
            FinishLoad(ELoadCompletionStatus.Failed);
        }

        return _loadingInfo;
    }

    protected virtual void PreLoad()
    {
        _loadingInfo = new LoadingInfo(GetPercent);
    }

    protected abstract void PerformLoad();

    protected void FinishLoad(ELoadCompletionStatus status)
    {
        _isLoading = false;
        
        _loadingInfo.Result = this;
        _loadingInfo.Status = status;
        _loadingInfo.Complete();
    }

    protected abstract int GetPercent();
}

public class LoadableData
{
    public Func<LoadingInfo> Loadable;
    public Action<LoadingInfo> Callback;

    public LoadableData(Func<LoadingInfo> loadable, Action<LoadingInfo> callback = null)
    {
        Loadable = loadable;
        Callback = callback;
    }
}
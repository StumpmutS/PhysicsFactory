using System;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadingInfo
{
    public object Result;
    public int Percentage => _percentGetter?.Invoke() ?? 0;
    public ELoadCompletionStatus Status;
    public Exception Exception;
    
    private Func<int> _percentGetter;

    public LoadingInfo(Func<int> percentGetter)
    {
        Result = null;
        _percentGetter = percentGetter;
        Status = ELoadCompletionStatus.None;
        Exception = null;
    }

    private event Action<LoadingInfo> _onComplete = delegate { };
    
    /// <summary>
    /// Invokes subscriber if loading already complete
    /// </summary>
    public event Action<LoadingInfo> OnComplete
    {
        add
        {
            _onComplete += value;
            if (Status != ELoadCompletionStatus.None) value.Invoke(this);
        }
        remove => _onComplete -= value;
    }

    public void Complete()
    {
        _onComplete.Invoke(this);
    }

    public static LoadingInfo From(AsyncOperationHandle asyncOperationHandle)
    {
        var info = new LoadingInfo(() => Mathf.RoundToInt(asyncOperationHandle.PercentComplete * 100));
        
        asyncOperationHandle.Completed += handle =>
        {
            info.Result = handle.Result;
            info.Status = handle.Status switch
            {
                AsyncOperationStatus.Succeeded => ELoadCompletionStatus.Succeeded,
                AsyncOperationStatus.Failed => ELoadCompletionStatus.Failed,
                _ => ELoadCompletionStatus.None
            };
            info.Exception = handle.OperationException;
            info.Complete();
        };

        return info;
    }
}
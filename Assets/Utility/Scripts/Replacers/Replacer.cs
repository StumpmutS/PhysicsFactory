using UnityEngine;

public abstract class Replacer<T> : MonoBehaviour
{
    private T _replacedData;
    
    public void InitReplacement(T replacementData)
    {
        if (!TryGetCurrentData(out var oldData) || !TryReplace(replacementData)) return;

        _replacedData = oldData;
    }

    protected abstract bool TryGetCurrentData(out T data);
    
    protected abstract bool TryReplace(T data);

    public void StopReplacement(T data)
    {
        if (_replacedData == null || !TryGetCurrentData(out var currentData) || !currentData.Equals(data)) return;

        TryReplace(_replacedData);
    }
}
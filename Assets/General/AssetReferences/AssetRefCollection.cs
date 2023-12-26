using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class AssetRefCollection
{
    [SerializeField] private List<AssetReference> references = new();

    private List<object> _loadedAssets;

    public int Add(AssetReference reference)
    {
        references.Add(reference);
        return references.Count - 1;
    }

    public T Get<T>(int index)
    {
        if (index >= _loadedAssets.Count)
        {
            Debug.LogError($"Requested index out of range, index: {index}, number of loaded assets: {_loadedAssets.Count}");
            return default;
        }
        var asset = _loadedAssets[index];
        if (asset is not T result)
        {
            Debug.LogError($"Asset at index {index} was not of specified type: {typeof(T).Name}");
            return default;
        }

        return result;
    }

    public AssetRefContainer<T> GetContainerized<T>(int index)
    {
        if (index < 0 || index >= _loadedAssets.Count || index >= references.Count)
        {
            Debug.LogError($"Requested index out of range, index: {index}, number of loaded assets: {_loadedAssets.Count}");
            return default;
        }
        var asset = _loadedAssets[index];
        if (asset is not T result)
        {
            Debug.LogError($"Asset at index {index} was not of specified type: {typeof(T).Name}");
            return default;
        }

        return new AssetRefContainer<T>(references[index], result);
    }

    public LoadingInfo LoadAll()
    {
        _loadedAssets = new List<object>(new object[references.Count]);

        return new UnorderedLoader(references.Select((r, i) =>
            new LoadableData(() => LoadingInfo.From(AssetRefHelpers.LoadOrGet<object>(r)),
                info => HandleReferenceLoaded(info, i)))).Load();
    }

    private void HandleReferenceLoaded(LoadingInfo info, int index)
    {
        if (info.Status != ELoadCompletionStatus.Succeeded)
        {
            Debug.LogError($"Problem loading asset reference: {references[index]}");
        }
        
        _loadedAssets[index] = info.Result;
    }
}
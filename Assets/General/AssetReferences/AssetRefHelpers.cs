using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class AssetRefHelpers
{
    private static Dictionary<string, object> _loadedAssetHandles = new();

    public static AsyncOperationHandle<T> LoadOrGet<T>(AssetReference reference)
    {
        if (_loadedAssetHandles.TryGetValue(reference.AssetGUID, out var value) &&
            value is AsyncOperationHandle<T> result) return result;
        
        var handle = reference.LoadAssetAsync<T>();
        handle.Completed += h => HandleLoadComplete(h, reference);
        return handle;
    }

    private static void HandleLoadComplete<T>(AsyncOperationHandle<T> handle, AssetReference reference)
    {
        if (handle.Status != AsyncOperationStatus.Succeeded) return;

        _loadedAssetHandles[reference.AssetGUID] = handle;
    }

    public static void HandleRefContainerLoaded<T>(LoadingInfo info, AssetRefContainer<T> refContainer)
    {
        if (info.Status == ELoadCompletionStatus.Failed || info.Result is not T result)
        {
            Debug.LogError($"Error loading asset reference, result was: {info.Result}");
            refContainer.Asset = default;
            return;
        }

        refContainer.Asset = result;
    }
}
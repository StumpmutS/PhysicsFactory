using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SaveHelpers
{
    public static IEnumerable<ISaveable<T>> GetSaveables<T>()
    {
        return UnityEngine.Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISaveable<T>>();
    }
    
    public static IEnumerable<ILoadable<T>> GetLoadables<T>()
    {
        return UnityEngine.Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ILoadable<T>>();
    }
    
    public static void GroupSave<T>(IEnumerable<ISaveable<T>> saveables, T data, AssetRefCollection assetRefCollection)
    {
        foreach (var saveable in saveables)
        {
            saveable.Save(data, assetRefCollection);
        }
    }
}
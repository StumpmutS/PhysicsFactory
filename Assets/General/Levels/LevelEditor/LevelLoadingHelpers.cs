using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;

public class LevelLoadingHelpers : Singleton<LevelLoadingHelpers>
{
    [SerializeField] private LevelOptionsSO editorOptionsSo;
    
    public static bool CompareOptions(LevelOptionsSO optionsSo, LevelData data) => CompareOptions(optionsSo.Options, data);
    
    public static bool CompareOptions(IDictionary<string, string> options, LevelData data)
    {
        bool match = true;
        foreach (var option in options)
        {
            if (data.Options.TryGetValue(option.Key, out var value) && value == option.Value) continue;
            match = false;
            break;
        }

        return match;
    }

    public static GameObject GetSaveableAsset(LevelData levelData, SaveableObjectSaveData saveableObjectSaveData, AssetRefCollection assetRefCollection)
    {
        var id = CompareOptions(Instance.editorOptionsSo, levelData)
            ? saveableObjectSaveData.PrefabReferenceIds[EPrefabSaveType.Editor]
            : saveableObjectSaveData.PrefabReferenceIds[EPrefabSaveType.Session];
        return assetRefCollection.Get<GameObject>(id);
    }
}
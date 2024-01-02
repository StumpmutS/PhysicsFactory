using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelEditor : MonoBehaviour, ILoadable<LevelData>
{
    [SerializeField] private LevelOptionsSO editorOptionsSo;
    
    public UnityEvent OnEditModeActive = new();
    
    public LoadingInfo Load(LevelData data, AssetRefCollection assetRefCollection)
    {
        var editing = LevelLoadingHelpers.CompareOptions(editorOptionsSo, data);
        
        if (editing) OnEditModeActive.Invoke();

        return LoadingInfo.Completed(editing, ELoadCompletionStatus.Succeeded);
    }
}

public static class LevelLoadingHelpers
{
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

    public static GameObject GetBuildingAsset(LevelOptionsSO editorOptionsSo, LevelData levelData, BuildingSaveData buildingSaveData, AssetRefCollection assetRefCollection)
    {
        var id = CompareOptions(editorOptionsSo, levelData)
            ? buildingSaveData.EditorBuildingPrefabReferenceId
            : buildingSaveData.SessionBuildingPrefabReferenceId;
        return assetRefCollection.Get<GameObject>(id);
    }
}
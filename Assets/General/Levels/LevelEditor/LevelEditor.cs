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
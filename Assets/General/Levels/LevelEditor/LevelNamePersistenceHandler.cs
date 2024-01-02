using TMPro;
using UnityEngine;

public class LevelNamePersistenceHandler : MonoBehaviour, ISaveable<LevelData>, ILoadable<LevelData>
{
    [SerializeField] private TMP_InputField levelNameInputField;
    
    public void Save(LevelData data, AssetRefCollection assetRefCollection)
    {
        data.LevelInfo.Name = levelNameInputField.text;
    }

    public LoadingInfo Load(LevelData data, AssetRefCollection assetRefCollection)
    {
        levelNameInputField.text = data.LevelInfo.Name;
        
        return LoadingInfo.Completed(data, ELoadCompletionStatus.Succeeded);
    }
}
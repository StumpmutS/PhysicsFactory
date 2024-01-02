using UnityEngine;
using Utility.Scripts;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;
    [SerializeField] private SceneInfoSO levelSceneInfo;
    [SerializeField] private LevelOptionsSO optionsSo;
    
    public void LoadLevel(LevelSO levelSo) => LoadLevel(levelSo.LevelData);
    public void LoadLevel(SaveData data) => LoadLevel(data.LevelData);
    
    public void LoadLevel(LevelData data)
    {
        if (data is null)
        {
            Debug.LogError("Level data was null");
            return;
        }
        
        data.Options ??= new SerializableDictionary<string, string>();
        foreach (var kvp in optionsSo.Options)
        {
            data.Options[kvp.Key] = kvp.Value;
        }
        
        LevelDataContainer.Instance.LevelData = data;
        sceneController.SetScene(levelSceneInfo);
    }
}
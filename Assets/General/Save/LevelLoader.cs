using UnityEngine;
using UnityEngine.Serialization;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private LevelDataContainer levelDataContainer;
    [SerializeField] private SceneController sceneController;
    [SerializeField] private int levelSceneIndex = 1;

    public void LoadLevel(LevelData data)
    {
        levelDataContainer.LevelData = data;
        sceneController.SetScene(levelSceneIndex);
    }
}

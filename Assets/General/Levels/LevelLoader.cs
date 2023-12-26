using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;
    [SerializeField] private int levelSceneIndex = 1;

    public void LoadLevel(LevelData data)
    {
        LevelDataContainer.Instance.LevelData = data;
        sceneController.SetScene(levelSceneIndex);
    }
}

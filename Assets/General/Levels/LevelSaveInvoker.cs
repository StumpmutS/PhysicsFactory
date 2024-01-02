using UnityEngine;

public class LevelSaveInvoker : MonoBehaviour
{
    public void Save()
    {
        LevelSaveManager.Instance.SaveLevel(LevelSaveHelpers.GetUpdatedLevelInfo());
    }
}
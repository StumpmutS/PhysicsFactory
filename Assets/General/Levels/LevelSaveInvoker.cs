using System;
using UnityEngine;

public class LevelSaveInvoker : MonoBehaviour
{
    public void Save()
    {
        var currentInfo = LevelDataHandler.Instance.Info;
        var newInfo = new LevelInfo(currentInfo.Name, new SerializableDateTime(DateTime.Now), currentInfo.SerializableGuid.Guid);
        LevelSaveManager.Instance.SaveLevel(newInfo);
    }
}
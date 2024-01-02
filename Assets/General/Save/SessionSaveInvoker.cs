using System;
using UnityEngine;

public class SessionSaveInvoker : MonoBehaviour
{
    [SerializeField] private string saveType;
    
    public void Save()
    {
        var levelInfo = LevelDataHandler.Instance.Info;

        SessionSaveManager.Instance.Save(new SaveInfo(levelInfo.Name, saveType,
            new SerializableDateTime(DateTime.Now)), levelInfo);
    }
}
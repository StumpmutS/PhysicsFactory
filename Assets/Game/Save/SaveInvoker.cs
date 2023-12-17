using System;
using UnityEngine;

public class SaveInvoker : MonoBehaviour
{
    [SerializeField] private string saveType;
    
    public void Save()
    {
        SaveManager.Instance.Save(
            new SaveInfo(
                new SaveDisplayData(
                    LevelManager.Instance.Name, 
                    saveType, 
                    new SerializableDateTime(DateTime.Now))));
    }
}
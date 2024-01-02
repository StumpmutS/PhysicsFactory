using System;
using UnityEngine;

[Serializable]
public class SaveDisplayData
{
    [SerializeField] private SaveData saveData;
    public SaveData SaveData => saveData;

    public SaveDisplayData(SaveData saveData)
    {
        this.saveData = saveData;
    }
}
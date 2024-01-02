using System;
using UnityEngine;

[Serializable]
public class LevelDisplayData
{
    [SerializeField] private LevelData levelData;
    public LevelData LevelData => levelData;

    public LevelDisplayData(LevelData levelData)
    {
        this.levelData = levelData;
    }
}
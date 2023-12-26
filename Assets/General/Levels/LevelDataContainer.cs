using System;
using UnityEngine;
using Utility.Scripts;

public class LevelDataContainer : Singleton<LevelDataContainer>
{
    public LevelData LevelData { get; set; }
    
    protected override void Awake()
    {
        base.Awake();
        if (gameObject != null) DontDestroyOnLoad(gameObject);
    }
}
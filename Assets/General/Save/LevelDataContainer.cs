using System;
using Utility.Scripts;

public class LevelDataContainer : Singleton<LevelDataContainer>
{
    public LevelData LevelData { get; set; }
    
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
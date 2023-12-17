using System;
using System.Collections.Generic;

[Serializable]
public class LevelData
{
    public LevelInfo LevelInfo = new();
    public SupplySaveData supplyData = new();
    public List<BuildingSaveData> BuildingSaveData = new();
}
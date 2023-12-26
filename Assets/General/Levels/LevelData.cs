using System;
using System.Collections.Generic;

[Serializable]
public class LevelData
{
    public LevelInfo LevelInfo = new();
    public AssetRefCollection AssetRefCollection = new();
    public SupplySaveData SupplyData = new();
    public List<BuildingSaveData> BuildingSaveData = new();

    public LevelData(AssetRefCollection assetRefCollection)
    {
        AssetRefCollection = assetRefCollection;
    }
}
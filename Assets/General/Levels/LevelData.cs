using System;
using System.Collections.Generic;
using UnityEngine.Serialization;
using Utility.Scripts;

[Serializable]
public class LevelData
{
    public LevelInfo LevelInfo;
    public AssetRefCollection AssetRefCollection;
    public SupplySaveData SupplyData = new();
    public List<SaveableObjectSaveData> SaveableObjectSaveData = new();
    public PlacementSaveData PlacementSaveData = new();
    public SerializableDictionary<string, string> Options = new();

    public LevelData(LevelInfo levelInfo, AssetRefCollection assetRefCollection)
    {
        LevelInfo = levelInfo;
        AssetRefCollection = assetRefCollection;
    }
}
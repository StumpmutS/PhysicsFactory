using System;
using UnityEngine.AddressableAssets;

[Serializable]
public class BuildingSaveData
{
    public AssetReference BuildingPrefabReference;
    public PlacedBuildingSaveData PlacedBuildingSaveData;
    public UpgradeSaveData UpgradeSaveData;
    public ModificationSaveData ModificationSaveData;
    public CurrentSaveData CurrentSaveData;
    public EnergySpreadSaveData EnergySpreadSaveData;
}
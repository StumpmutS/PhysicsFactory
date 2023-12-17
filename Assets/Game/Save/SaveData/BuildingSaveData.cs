using System;
using UnityEngine.AddressableAssets;

[Serializable]
public class BuildingSaveData
{
    public AssetReference BuildingPrefabReference;
    public PlacedBuildingInfo BuildingInfo;
    public UpgradeSaveData UpgradeSaveData;
    public ModificationSaveData ModificationSaveData;
    public CurrentSaveData CurrentSaveData;
    public EnergySpreadSaveData EnergySpreadSaveData;
}

[Serializable]
public class UpgradeSaveData
{
    public string Placeholder;
}

[Serializable]
public class ModificationSaveData
{
    public string Placeholder;
}

[Serializable]
public class CurrentSaveData
{
    public string Placeholder;
}

[Serializable]
public class EnergySpreadSaveData
{
    public string Placeholder;
}

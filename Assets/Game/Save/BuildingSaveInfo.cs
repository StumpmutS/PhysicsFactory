using System;
using UnityEngine.AddressableAssets;

[Serializable]
public class BuildingSaveInfo
{
    public AssetReference PrefabReference;
    public PlacedBuildingInfo BuildingInfo;
}
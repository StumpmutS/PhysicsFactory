﻿using System;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

[Serializable]
public class BuildingSaveData
{
    public IdentifiableObjectSaveData IdentifiableObjectSaveData;
    public int EditorBuildingPrefabReferenceId = -1;
    public int SessionBuildingPrefabReferenceId = -1;
    public PlacedBuildingSaveData PlacedBuildingSaveData;
    public UpgradeSaveData UpgradeSaveData;
    public ModificationSaveData ModificationSaveData;
    public GeneratorSaveData GeneratorSaveData;
    public CurrentSaveData CurrentSaveData;
    public EnergySpreadSaveData EnergySpreadSaveData;
}
using System;

[Serializable]
public class PlaceableSaveData
{
    public IdentifiableObjectSaveData IdentifiableObjectSaveData;
    public int EditorPlaceablePrefabReferenceId = -1;
    public int SessionPlaceablePrefabReferenceId = -1;
    public PlacedSaveData PlacedSaveData;
    public UpgradeSaveData UpgradeSaveData;
    public ModificationSaveData ModificationSaveData;
    public GeneratorSaveData GeneratorSaveData;
    public CurrentSaveData CurrentSaveData;
    public EnergySpreadSaveData EnergySpreadSaveData;
}
using System;
using Utility.Scripts;

[Serializable]
public class SaveableObjectSaveData
{
    public int Id;
    public SerializableDictionary<EPrefabSaveType, int> PrefabReferenceIds = new();
    public PlacedSaveData PlacedSaveData;
    public UpgradeSaveData UpgradeSaveData;
    public ModificationSaveData ModificationSaveData;
    public GeneratorSaveData GeneratorSaveData;
    public CurrentSaveData CurrentSaveData;
    public EnergySpreadSaveData EnergySpreadSaveData;
    public ExtractorSaveData ExtractorSaveData;

    public SaveableObjectSaveData(int id = -1)
    {
        Id = id;
    }
}
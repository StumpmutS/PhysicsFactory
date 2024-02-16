using System;
using UnityEngine.Serialization;
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
    public NodeSaveData NodeSaveData;
    public EnergySpreadSaveData EnergySpreadSaveData;
    public ExtractorSaveData ExtractorSaveData;

    public SaveableObjectSaveData(int id = -1)
    {
        Id = id;
    }
}
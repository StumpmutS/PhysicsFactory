using UnityEngine;

public class GeneratorPersistenceHandler : MonoBehaviour, ISaveable<SaveableObjectSaveData>, ILoadable<GeneratorSaveData>
{
    [SerializeField] private EnergyGenerator generator;
    [SerializeField] private EnergyNodeAutoConnector autoConnector;
    [SerializeField] private EnergyNodeFinder finder;
    
    public void Save(SaveableObjectSaveData data, AssetRefCollection assetRefCollection)
    {
        data.GeneratorSaveData ??= new GeneratorSaveData();
        data.GeneratorSaveData.ChargeGenerated = generator.ChargeGenerated;
        data.GeneratorSaveData.Locked = autoConnector.Locked;
        data.GeneratorSaveData.Range = finder.Range;
    }

    public LoadingInfo Load(GeneratorSaveData data, AssetRefCollection assetRefCollection)
    {
        generator.Load(data.ChargeGenerated);
        autoConnector.Locked = data.Locked;
        finder.Range = data.Range;

        return LoadingInfo.Completed(data, ELoadCompletionStatus.Succeeded);
    }
}
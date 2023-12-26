﻿using UnityEngine;

public class GeneratorPersistenceHandler : MonoBehaviour, ISaveable<BuildingSaveData>, ILoadable<GeneratorSaveData>
{
    [SerializeField] private EnergyNodeAutoConnector autoConnector;
    [SerializeField] private EnergyNodeFinder finder;
    
    public void Save(BuildingSaveData data, AssetRefCollection assetRefCollection)
    {
        data.GeneratorSaveData ??= new GeneratorSaveData();
        data.GeneratorSaveData.Locked = autoConnector.Locked;
        data.GeneratorSaveData.Range = finder.Range;
    }

    public LoadingInfo Load(GeneratorSaveData data, AssetRefCollection assetRefCollection)
    {
        autoConnector.Locked = data.Locked;
        finder.Range = data.Range;

        var info = new LoadingInfo(() => 100)
        {
            Result = data,
            Status = ELoadCompletionStatus.Succeeded
        };
        info.Complete();
        return info;
    }
}
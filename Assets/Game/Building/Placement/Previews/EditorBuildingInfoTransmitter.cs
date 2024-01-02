using UnityEngine;
using UnityEngine.AddressableAssets;

public class EditorBuildingInfoTransmitter : BuildingInfoTransmitter
{
    [SerializeField] private AssetReference levelEditorPrefabReference;
    
    protected override void SaveData(BuildingSaveData data, AssetRefCollection assetRefCollection)
    {
        base.SaveData(data, assetRefCollection);
        data.EditorBuildingPrefabReferenceId = assetRefCollection.Add(levelEditorPrefabReference);
    }
}
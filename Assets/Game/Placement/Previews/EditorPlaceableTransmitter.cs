using UnityEngine;
using UnityEngine.AddressableAssets;

public class EditorPlaceableTransmitter : PlaceableTransmitter
{
    [SerializeField] private AssetReference levelEditorPrefabReference;
    
    protected override void SaveData(PlaceableSaveData data, AssetRefCollection assetRefCollection)
    {
        base.SaveData(data, assetRefCollection);
        data.EditorPlaceablePrefabReferenceId = assetRefCollection.Add(levelEditorPrefabReference);
    }
}
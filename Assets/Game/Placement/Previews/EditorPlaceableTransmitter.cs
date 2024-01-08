using UnityEngine;
using UnityEngine.AddressableAssets;

public class EditorPlaceableTransmitter : PlaceableTransmitter
{
    [SerializeField] private AssetReference levelEditorPrefabReference;
    
    protected override void SavePrefabData(SaveableObjectSaveData data, AssetRefCollection assetRefCollection)
    {
        base.SavePrefabData(data, assetRefCollection);
        data.PrefabReferenceIds[EPrefabSaveType.Editor] = assetRefCollection.Add(levelEditorPrefabReference);
    }
}
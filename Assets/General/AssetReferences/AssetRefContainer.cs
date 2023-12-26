using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

[Serializable]
public class AssetRefContainer<T>
{
    [SerializeField] private AssetReference reference;
    public AssetReference Reference => reference;
    [FormerlySerializedAs("Data")] public T Asset;

    public AssetRefContainer(AssetReference reference, T asset)
    {
        this.reference = reference;
        Asset = asset;
    }
}

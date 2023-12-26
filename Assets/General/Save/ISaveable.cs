using System.Collections.Generic;
using UnityEngine.AddressableAssets;

public interface ISaveable<TData>
{
    public void Save(TData data, AssetRefCollection assetRefCollection);
}
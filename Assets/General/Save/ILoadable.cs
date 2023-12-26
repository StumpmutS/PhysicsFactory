public interface ILoadable<TData>
{
    public LoadingInfo Load(TData data, AssetRefCollection assetRefCollection);
}
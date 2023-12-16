using System;

public interface ILoadable<TData>
{
    public event Action<ILoadable<TData>> OnLoadComplete;
    
    public void Load(TData data);
}
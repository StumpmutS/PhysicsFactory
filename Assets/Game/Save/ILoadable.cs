using System;

public interface ILoadable
{
    public event Action<ILoadable> OnLoadComplete;
    
    public void Load(SaveData data);
}
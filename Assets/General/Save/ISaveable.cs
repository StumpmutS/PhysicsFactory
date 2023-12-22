using UnityEngine.Serialization;

public interface ISaveable<TData>
{
    public void Save(TData data);
}
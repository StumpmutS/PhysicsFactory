using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class LevelInfo
{
    [SerializeField] public SerializableGuid SerializableGuid;
    public string Id => SerializableGuid.Guid.ToString();
    public string Name;
    public SerializableDateTime SerializableLastUpdatedTimeStamp;
    public DateTime LastUpdatedTimeStamp => SerializableLastUpdatedTimeStamp.DateTime;

    public LevelInfo(string name, SerializableDateTime lastUpdatedTimeStamp, Guid guid = default)
    {
        SerializableGuid = new SerializableGuid(guid == default ? Guid.NewGuid() : guid);
        Name = name;
        SerializableLastUpdatedTimeStamp = lastUpdatedTimeStamp;
    }
}
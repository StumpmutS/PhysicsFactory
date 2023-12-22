using System;
using UnityEngine;
using UnityEngine.Serialization;
using Utility.Scripts;

[Serializable]
public class SaveInfo
{
    [SerializeField] private SerializableGuid guid;
    public string Id => guid.Guid.ToString();
    [SerializeField] private string name;
    public string Name => name;
    [SerializeField] private string saveType;
    public string SaveType => saveType;
    [SerializeField] private SerializableDateTime dateTime;
    public SerializableDateTime DateTime => dateTime;
    public SerializableDictionary<string, string> Options;

    public SaveInfo(string name, string saveType, SerializableDateTime dateTime, Guid guid = default)
    {
        this.guid = new SerializableGuid(guid == default ? Guid.NewGuid() : guid);
        this.name = name;
        this.saveType = saveType;
        this.dateTime = dateTime;
        Options = new SerializableDictionary<string, string>();
    }
}
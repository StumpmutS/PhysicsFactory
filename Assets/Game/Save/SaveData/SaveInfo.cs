using System;
using UnityEngine;
using UnityEngine.Serialization;
using Utility.Scripts;

[Serializable]
public class SaveInfo
{
    [SerializeField] private SerializableGuid guid;
    public string Id => guid.Guid.ToString();
    public SaveDisplayData displayData;
    public SerializableDictionary<string, string> Options;

    public SaveInfo(SaveDisplayData displayData, Guid guid = default)
    {
        this.guid = new SerializableGuid(guid == default ? Guid.NewGuid() : guid);
        this.displayData = displayData;
        Options = new SerializableDictionary<string, string>();
    }
}
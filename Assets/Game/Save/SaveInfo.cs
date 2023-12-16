using System;
using UnityEngine;
using Utility.Scripts;

[Serializable]
public class SaveInfo
{
    [SerializeField] private SerializableGuid guid;
    public string Id => guid.Guid.ToString();
    public SaveDisplayInfo DisplayInfo;
    public SerializableDictionary<string, string> Options;

    public SaveInfo(SaveDisplayInfo displayInfo, Guid guid = default)
    {
        this.guid = new SerializableGuid(guid == default ? Guid.NewGuid() : guid);
        DisplayInfo = displayInfo;
        Options = new SerializableDictionary<string, string>();
    }
}
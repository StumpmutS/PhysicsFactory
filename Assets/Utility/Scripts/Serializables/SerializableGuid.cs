using System;
using UnityEngine;

[Serializable]
public class SerializableGuid
{
    [SerializeField] private string stringGuid;

    public Guid Guid
    {
        get
        {
            if (Guid.TryParse(stringGuid, out var guid)) return guid;
            
            Debug.LogError($"Could not parse GUID from \"{stringGuid}\"");
            return default;
        }
    }

    public SerializableGuid(Guid guid)
    {
        stringGuid = guid.ToString();
    }
}
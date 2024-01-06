using System;
using UnityEngine;

[Serializable]
public class ResourceData
{
    [SerializeField] private EResourceType resourceType;
    public EResourceType ResourceType => resourceType;
    
    public float Amount;

    public static ResourceData CopyFrom(ResourceData resourceData)
    {
        return new ResourceData()
        {
            resourceType = resourceData.resourceType,
            Amount = resourceData.Amount
        };
    }
}
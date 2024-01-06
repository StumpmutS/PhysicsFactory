using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;

public class ResourceManager : Singleton<ResourceManager>
{
    [SerializeField] private SerializableDictionary<EResourceType, ListWrapper<ResourceModifier>> resourceModifiers;
    
    public ResourceData ModifiedResourceData(ResourceData resourceData)
    {
        if (!resourceModifiers.TryGetValue(resourceData.ResourceType, out var modifiers))
        {
            Debug.LogWarning($"Resource type {resourceData.ResourceType} does not have any assigned modifiers");
            return resourceData;
        }
        
        return ResourceModifierHelpers.ModifiedResourceData(resourceData, modifiers.List);
    }
}
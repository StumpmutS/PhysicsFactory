using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/GameObject/ResourceType")]
public class ResourceTypeRestriction : GameObjectRestriction
{
    [SerializeField] private bool matchType;
    [SerializeField, ShowIf(nameof(matchType), true)] private EResourceType matchingType;
    [SerializeField] private List<EResourceType> excludes;
    
    protected override ERestrictionFailureType RestrictionFailureType => ERestrictionFailureType.None;
    
    protected override bool Check(GameObject go, RestrictionFailureInfo failureInfo)
    {
        if (!go.TryGetComponent<Resource>(out var resource)) return !matchType;

        var resourceType = resource.Data.ResourceType;
        
        if (matchType && resourceType != matchingType) return false;
        foreach (var excludedType in excludes)
        {
            if (excludedType == resourceType) return false;
        }

        return true;
    }
}
using System.Collections.Generic;

public static class ResourceModifierHelpers
{
    public static ResourceData ModifiedResourceData(ResourceData resourceData, IEnumerable<ResourceModifier> modifiers)
    {
        var dataCopy = ResourceData.CopyFrom(resourceData);

        foreach (var modifier in modifiers)
        {
            modifier.Modify(dataCopy);
        }

        return dataCopy;
    }
}
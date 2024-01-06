using UnityEngine;

public abstract class ResourceModifier : ScriptableObject
{
    public abstract void Modify(ResourceData resourceData);
}
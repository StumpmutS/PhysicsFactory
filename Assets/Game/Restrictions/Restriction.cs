using UnityEngine;

public abstract class Restriction<T> : ScriptableObject
{
    protected abstract ERestrictionFailureType RestrictionFailureType { get; }

    public bool CheckRestriction(T restrictionInfo, RestrictionFailureInfo failureInfo)
    {
        if (Check(restrictionInfo, failureInfo)) return true;
        failureInfo.Failed = true;
        failureInfo.FailureType |= RestrictionFailureType;
        return false;
    }

    protected abstract bool Check(T restrictionInfo, RestrictionFailureInfo failureInfo);
    public virtual void PassRestriction(T info) { }
}
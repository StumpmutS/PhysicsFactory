using UnityEngine;

public abstract class BuildingRestriction : ScriptableObject
{
    public abstract bool CheckRestriction(BuildingRestrictionInfo info);
    public virtual void PassRestriction(BuildingRestrictionInfo info) { }
}
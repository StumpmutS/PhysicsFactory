using UnityEngine;

public abstract class Restriction : ScriptableObject
{
    public abstract bool CheckRestriction();
    public virtual void PassRestriction() { }
}
using UnityEngine;

public abstract class Restriction<T> : ScriptableObject
{
    public abstract bool CheckRestriction(T info);
    public virtual void PassRestriction(T info) { }
}
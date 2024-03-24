using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public abstract void ApplyEffect(GameObject go);
    public abstract void RemoveEffect(GameObject go);
}
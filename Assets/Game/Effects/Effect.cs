using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public abstract void ApplyEffect(EffectData data);
    public abstract void RemoveEffect(EffectData data);
}

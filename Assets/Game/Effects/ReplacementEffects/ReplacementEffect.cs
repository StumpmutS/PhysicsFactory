using UnityEngine;
using Utility.Scripts.Extensions;

public abstract class ReplacementEffect<TData, TReplacer> : Effect where TReplacer : Replacer<TData>
{
    protected abstract TData Data { get; }
    
    public override void ApplyEffect(EffectData effectData)
    {
        var replacer = effectData.GameObject.AddOrGetComponent<TReplacer>();
        replacer.InitReplacement(Data);
    }

    public override void RemoveEffect(EffectData effectData)
    {
        if (effectData.GameObject.TryGetComponent<TReplacer>(out var replacer))
        {
            replacer.StopReplacement(Data);
        }
    }
}
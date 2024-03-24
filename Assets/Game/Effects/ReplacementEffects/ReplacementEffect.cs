using UnityEngine;
using Utility.Scripts.Extensions;

public abstract class ReplacementEffect<TData, TReplacer> : Effect where TReplacer : Replacer<TData>
{
    [SerializeField] private TData data;
    
    public override void ApplyEffect(GameObject go)
    {
        var replacer = go.AddOrGetComponent<TReplacer>();
        replacer.InitReplacement(data);
    }

    public override void RemoveEffect(GameObject go)
    {
        if (go.TryGetComponent<TReplacer>(out var replacer))
        {
            replacer.StopReplacement(data);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

public abstract class Affector : MonoBehaviour
{
    [SerializeField] private List<Effect> effects;
    [SerializeField] private List<GameObjectRestriction> applicationRestrictions;
    [SerializeField] private List<GameObjectRestriction> removalRestrictions;
    
    protected void TryApplyEffects(EffectData data)
    {
        if (!RestrictionHelper.TryPassRestrictions(applicationRestrictions, data.GameObject, new RestrictionFailureInfo())) return;
        
        foreach (var effect in effects)
        {
            effect.ApplyEffect(data);
        }
    }

    protected void RemoveEffects(EffectData data)
    {
        if (!RestrictionHelper.TryPassRestrictions(removalRestrictions, data.GameObject, new RestrictionFailureInfo())) return;

        foreach (var effect in effects)
        {
            effect.RemoveEffect(data);
        }
    }
}
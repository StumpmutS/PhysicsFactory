using System.Collections.Generic;
using UnityEngine;

public abstract class Affector : MonoBehaviour
{
    [SerializeField] private List<Effect> effects;
    [SerializeField] private List<GameObjectRestriction> applicationRestrictions;
    [SerializeField] private List<GameObjectRestriction> removalRestrictions;

    protected void TryApplyEffects(GameObject go)
    {
        if (!RestrictionHelper.TryPassRestrictions(applicationRestrictions, go, new RestrictionFailureInfo())) return;
        
        foreach (var effect in effects)
        {
            effect.ApplyEffect(go);
        }
    }

    protected void RemoveEffects(GameObject go)
    {
        if (!RestrictionHelper.TryPassRestrictions(removalRestrictions, go, new RestrictionFailureInfo())) return;

        foreach (var effect in effects)
        {
            effect.RemoveEffect(go);
        }
    }
}
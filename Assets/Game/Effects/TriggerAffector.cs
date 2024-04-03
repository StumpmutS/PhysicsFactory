using UnityEngine;
using Utility.Scripts;

public class TriggerAffector : Affector
{
    private void OnTriggerEnter(Collider other)
    {
        var effectOrigin = Vector3.zero;
        if (other is BoxCollider boxCollider) effectOrigin = StumpRandom.SampleWorldPointInBoxCollider(boxCollider);
        
        TryApplyEffects(new EffectData(other.gameObject, effectOrigin));
    }

    private void OnTriggerStay(Collider other)
    {
        var effectOrigin = Vector3.zero;
        if (other is BoxCollider boxCollider) effectOrigin = StumpRandom.SampleWorldPointInBoxCollider(boxCollider);

        TryApplyEffects(new EffectData(other.gameObject, effectOrigin));
    }

    private void OnTriggerExit(Collider other)
    {
        var effectOrigin = Vector3.zero;
        if (other is BoxCollider boxCollider) effectOrigin = StumpRandom.SampleWorldPointInBoxCollider(boxCollider);

        RemoveEffects(new EffectData(other.gameObject, effectOrigin));
    }
}
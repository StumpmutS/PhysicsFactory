using System;
using UnityEngine;
using Utility.Scripts;

[RequireComponent(typeof(BoxCollider))]
public class TriggerAffector : Affector
{
    private BoxCollider _collider;
    
    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleTrigger(other);
    }

    private void OnTriggerStay(Collider other)
    {
        HandleTrigger(other);
    }

    private void HandleTrigger(Collider other)
    {
        var effectOrigin = StumpRandom.SampleWorldPointInBoxCollider(_collider);
        TryApplyEffects(new EffectData(other.gameObject, effectOrigin));
    }

    private void OnTriggerExit(Collider other)
    {
        var effectOrigin = Vector3.zero;
        if (other is BoxCollider boxCollider) effectOrigin = StumpRandom.SampleWorldPointInBoxCollider(boxCollider);

        RemoveEffects(new EffectData(other.gameObject, effectOrigin));
    }
}
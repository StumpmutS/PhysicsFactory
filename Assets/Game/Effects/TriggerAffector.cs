using System;
using UnityEngine;
using UnityEngine.Serialization;

public class TriggerAffector : Affector
{
    private void OnTriggerEnter(Collider other)
    {
        TryApplyEffects(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        TryApplyEffects(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveEffects(other.gameObject);
    }
}
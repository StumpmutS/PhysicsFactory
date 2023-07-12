using System;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyAffectorContainer : MonoBehaviour
{
    private Dictionary<Type, RigidbodyAffector> _affectors = new();

    private void Awake()
    {
        foreach (var affector in GetComponentsInChildren<RigidbodyAffector>())
        {
            AddAffector(affector);
        }
    }

    public void AddAffector(RigidbodyAffector affector)
    {
        var type = affector.GetType();
        
        if (affector.gameObject.scene.rootCount == 0)
        {
            affector = Instantiate(affector, transform);
        }

        if (_affectors.ContainsKey(type))
        {
            Destroy(_affectors[type]);
        }
        
        _affectors[affector.GetType()] = affector;
    }

    public void ActivateAffectors(Collision collision)
    {
        foreach (var affector in _affectors.Values)
        {
            if (!affector.isActiveAndEnabled) continue;
            affector.AffectRigidbody(collision);
        }
    }

    public void ContinuouslyActivateAffectors(Collision collision)
    {
        foreach (var affector in _affectors.Values)
        {
            if (!affector.isActiveAndEnabled) continue;
            affector.ContinuouslyAffectRigidbody(collision);
        }
    }

    public void DeactivateAffectors(Collision collision)
    {
        foreach (var affector in _affectors.Values)
        {
            if (!affector.isActiveAndEnabled) continue;
            affector.UnaffectRigidbody(collision);
        }
    }
    
    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.rigidbody == null) return;

        ActivateAffectors(collisionInfo);
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.rigidbody == null) return;

        ContinuouslyActivateAffectors(collisionInfo);
    }

    private void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.rigidbody == null) return;

        DeactivateAffectors(collisionInfo);
    }
}
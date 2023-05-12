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

    public void ActivateAffectors(Rigidbody rigidbody)
    {
        foreach (var affector in _affectors.Values)
        {
            if (!affector.isActiveAndEnabled) continue;
            affector.AffectRigidbody(rigidbody);
        }
    }

    public void ContinuouslyActivateAffectors(Rigidbody rigidbody)
    {
        foreach (var affector in _affectors.Values)
        {
            if (!affector.isActiveAndEnabled) continue;
            affector.ContinuouslyAffectRigidbody(rigidbody);
        }
    }

    public void DeactivateAffectors(Rigidbody rigidbody)
    {
        foreach (var affector in _affectors.Values)
        {
            if (!affector.isActiveAndEnabled) continue;
            affector.UnaffectRigidbody(rigidbody);
        }
    }
    
    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.rigidbody == null) return;

        ActivateAffectors(collisionInfo.rigidbody);
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.rigidbody == null) return;

        ContinuouslyActivateAffectors(collisionInfo.rigidbody);
    }

    private void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.rigidbody == null) return;

        DeactivateAffectors(collisionInfo.rigidbody);
    }
}
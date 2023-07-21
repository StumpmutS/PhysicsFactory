using System;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyAffectorContainer : MonoBehaviour
{
    [SerializeField] private Transform containerTransform;
    
    private Dictionary<Type, RigidBodyAffector> _affectors = new();

    private void Awake()
    {
        foreach (var affector in GetComponentsInChildren<RigidBodyAffector>())
        {
            AddAffector(affector);
        }
    }

    public RigidBodyAffector AddAffector(RigidBodyAffector affectorPrefab)
    {
        var type = affectorPrefab.GetType();
        var affector = Instantiate(affectorPrefab, containerTransform);
        if (_affectors.TryGetValue(type, out var currentAffector)) Destroy(currentAffector.gameObject);
        _affectors[type] = affector;
        return affector;
    }

    public void RemoveAffector(RigidBodyAffector affector)
    {
        if (_affectors.TryGetValue(affector.GetType(), out var rigidBodyAffector))
        {
            _affectors.Remove(affector.GetType());
            Destroy(rigidBodyAffector.gameObject);
        }
    }

    private void ActivateAffectors(Collision collision)
    {
        foreach (var affector in _affectors.Values)
        {
            if (!affector.isActiveAndEnabled) continue;
            affector.AffectRigidbody(collision);
        }
    }

    private void ContinuouslyActivateAffectors(Collision collision)
    {
        foreach (var affector in _affectors.Values)
        {
            if (!affector.isActiveAndEnabled) continue;
            affector.ContinuouslyAffectRigidbody(collision);
        }
    }

    private void DeactivateAffectors(Collision collision)
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
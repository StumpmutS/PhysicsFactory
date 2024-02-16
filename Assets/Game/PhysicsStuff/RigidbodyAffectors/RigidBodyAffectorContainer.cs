using System;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyAffectorContainer : MonoBehaviour
{
    [SerializeField] private Transform containerTransform;
    
    private Dictionary<Type, RigidBodyAffector> _affectors = new();

    private void Awake()
    {
        foreach (var affector in containerTransform.GetComponentsInChildren<RigidBodyAffector>())
        {
            SetAffector(affector.GetType(), affector);
        }
    }

    public RigidBodyAffector AddAffector(RigidBodyAffector affectorPrefab)
    {
        var affector = Instantiate(affectorPrefab, containerTransform);
        affector.Init(this);
        SetAffector(affectorPrefab.GetType(), affector);
        return affector;
    }

    private void SetAffector(Type type, RigidBodyAffector affector)
    {
        if (_affectors.TryGetValue(type, out var currentAffector)) Destroy(currentAffector.gameObject);
        _affectors[type] = affector;
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
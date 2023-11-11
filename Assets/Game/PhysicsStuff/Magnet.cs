using System;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private float startingGravity;
    [SerializeField] private float maxInfluenceDistance;
    [SerializeField] private LayerMask layer;

    private float _gravity;

    private void Awake()
    {
        SetGravity(startingGravity);
    }
    
    private readonly Collider[] _colliders = new Collider[1024];
    private void FixedUpdate()
    {
        Array.Clear(_colliders, 0, _colliders.Length);
        Physics.OverlapSphereNonAlloc(transform.position, maxInfluenceDistance, _colliders, layer);
        foreach (var collider in _colliders)
        {
            if (collider == null || collider.attachedRigidbody == null) continue;

            var direction = (transform.position - collider.transform.position).normalized;
            
            collider.attachedRigidbody.AddForce(direction * _gravity / direction.sqrMagnitude);
        }
    }

    public void SetGravity(float value)
    {
        _gravity = value;
    }
}

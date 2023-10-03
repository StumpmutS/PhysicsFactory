using System.Collections.Generic;
using UnityEngine;

public static class NodeFinderHelper
{
    public static List<EnergyNode> FindNodesInRange(float range, Transform transform, Vector3 offset, LayerMask layerMask)
    {
        var colliders = new Collider[100];
        var extents = (range + .5f) * Vector3.one - .01f * Vector3.one;
        Physics.SyncTransforms();
        Physics.OverlapBoxNonAlloc(transform.position + offset, extents, colliders, Quaternion.identity, layerMask);
        
        List<EnergyNode> nodes = new();
        foreach (var collider in colliders)
        {
            if (collider == null || !collider.TryGetComponent<EnergyNode>(out var other)) continue;
            nodes.Add(other);
        }
        return nodes;
    }
}
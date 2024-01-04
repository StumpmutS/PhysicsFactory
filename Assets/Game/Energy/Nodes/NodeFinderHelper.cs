using System.Collections.Generic;
using UnityEngine;

public static class NodeFinderHelper
{
    private static readonly Collider[] Colliders = new Collider[1024];
    
    public static List<EnergyNode> FindNodesInRange(float range, Transform transform, Vector3 offset, LayerMask layerMask)
    {
        List<EnergyNode> nodes = new();
        var extents = (range + .5f) * Vector3.one - .01f * Vector3.one;
        Physics.SyncTransforms();
        var found = Physics.OverlapBoxNonAlloc(transform.position + offset, extents, Colliders, Quaternion.identity, layerMask);
        
        for (int i = 0; i < found; i++)
        {
            var collider = Colliders[i];
            if (collider == null || !collider.TryGetComponent<EnergyNode>(out var other)) continue;
            nodes.Add(other);
        }

        return nodes;
    }
}
using System.Collections.Generic;
using UnityEngine;

public class GeneratorNode : EnergyNode
{
    [SerializeField] private EnergyNodeFinder nodeFinder;
    
    public override bool CanConnect(EnergyNode other, out CurrentContainer sender, out CurrentContainer receiver)
    {
        sender = CurrentContainer;
        receiver = other.CurrentContainer;
        return other is not GeneratorNode && nodeFinder.Nodes.Contains(other);
    }
}
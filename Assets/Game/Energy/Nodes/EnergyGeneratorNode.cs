using System.Collections.Generic;
using UnityEngine;

public class EnergyGeneratorNode : EnergyNode
{
    [SerializeField] private EnergyGenerator generator;
    public EnergyGenerator Generator => generator;

    protected override void CheckSelected(Selectable selectable)
    {
        if (selectable.MainObject.TryGetComponent<EnergyContainerNode>(out var node))
        {
            InitiateCurrent(generator, node.Container, node);
        }
    }
}
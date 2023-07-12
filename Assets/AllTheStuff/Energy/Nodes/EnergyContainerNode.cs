using UnityEngine;

public class EnergyContainerNode : EnergyNode
{
    [SerializeField] private EnergyContainer container;
    public EnergyContainer Container => container;

    protected override void CheckSelected(Selectable selectable)
    {
        if (selectable.MainObject.TryGetComponent<EnergyGeneratorNode>(out var node))
        {
            InitiateCurrent(node.Generator, container, node);
        }
    }
}
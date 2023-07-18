using UnityEngine;

public class EnergyContainerNode : EnergyNode
{
    [SerializeField] private EnergyContainer container;
    public EnergyContainer Container => container;
}
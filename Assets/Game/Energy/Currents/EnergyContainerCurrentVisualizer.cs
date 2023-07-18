using System.Collections.Generic;
using UnityEngine;

public class EnergyContainerCurrentVisualizer : EnergyCurrentVisualizer
{
    [SerializeField] private EnergyContainer container;

    protected override HashSet<EnergyCurrent> Currents => container.Currents;
}
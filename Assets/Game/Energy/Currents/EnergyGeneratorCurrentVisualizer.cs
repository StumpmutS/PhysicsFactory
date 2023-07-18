using System.Collections.Generic;
using UnityEngine;

public class EnergyGeneratorCurrentVisualizer : EnergyCurrentVisualizer
{
    [SerializeField] private EnergyGenerator generator;

    protected override HashSet<EnergyCurrent> Currents => generator.Currents;
}
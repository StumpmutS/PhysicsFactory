using System.Collections.Generic;
using UnityEngine;

public class EnergyGeneratorNode : EnergyNode
{
    [SerializeField] private EnergyGenerator generator;
    public EnergyGenerator Generator => generator;
}
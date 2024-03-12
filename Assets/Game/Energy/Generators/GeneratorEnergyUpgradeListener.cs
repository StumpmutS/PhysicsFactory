using UnityEngine;
using Utility.Scripts;

public class GeneratorEnergyUpgradeListener : UpgradeListener
{
    [SerializeField] private EnergyGenerator generator;
    [SerializeField] private SerializableDictionary<int, float> energyRanges;
    
    protected override void HandleUpgradeLevel(int level)
    {
        generator.ChargeGenerated = energyRanges[level];
    }
}
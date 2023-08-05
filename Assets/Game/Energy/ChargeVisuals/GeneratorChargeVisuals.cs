using UnityEngine;

public class GeneratorChargeVisuals : ChargeVisuals
{
    [SerializeField] private EnergyGenerator generator;

    protected override string TextValue => generator.EnergyGenerated.ToString("F2");
}
using UnityEngine;

public class StorageChargeVisuals : ChargeVisuals
{
    [SerializeField] private EnergyStorage storage;

    protected override string TextValue => storage.TotalCharge.ToString("F2");
}
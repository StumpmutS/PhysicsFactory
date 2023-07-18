using UnityEngine;

public class BuildingVolumeEnergyConverter : EnergyConverter
{
    [SerializeField] private Building building;
    
    public override float ConvertEnergy(float charge)
    {
        return charge / building.Info.Volume;
    }
}
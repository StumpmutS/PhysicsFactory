using UnityEngine;
using UnityEngine.Serialization;

public class VolumeEnergyConverter : EnergyConverter
{
    [FormerlySerializedAs("building")] [SerializeField] private Placeable placeable;
    
    public override float ConvertEnergy(float charge)
    {
        return charge / placeable.Data.Volume;
    }
}
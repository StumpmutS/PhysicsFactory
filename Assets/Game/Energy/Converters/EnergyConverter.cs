using UnityEngine;

public abstract class EnergyConverter : MonoBehaviour
{
    public abstract float ConvertEnergy(float charge);
    public abstract float UnconvertEnergy(float charge);
}
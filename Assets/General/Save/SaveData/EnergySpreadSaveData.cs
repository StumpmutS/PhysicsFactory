using System;
using Utility.Scripts;

[Serializable]
public class EnergySpreadSaveData
{
    public float MaxTotal;
    public SerializableDictionary<string, SerializableSignedFloat> Spread;
}
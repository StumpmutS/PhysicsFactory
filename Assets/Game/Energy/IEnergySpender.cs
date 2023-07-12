using System;

public interface IEnergySpender
{
    public void SetEnergyLevel(int amount);
    public EnergySpenderInfo Info { get; }
}

[Serializable]
public class EnergySpenderInfo
{
    public string Label;
    
    public EnergySpenderInfo(string label)
    {
        Label = label;
    }
}
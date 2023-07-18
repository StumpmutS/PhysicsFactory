using System;

public interface IEnergySpender
{
    public void SetEnergyLevel(float amount);
    public EnergySpenderInfo SpenderInfo { get; }
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
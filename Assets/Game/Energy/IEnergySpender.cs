using System;

public interface IEnergySpender
{
    public void SetEnergyLevel(float amount);
    public ContextData Context { get; }
}

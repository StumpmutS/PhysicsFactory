using UnityEngine;

public static class SupplyCalculator
{
    public static float CalculatePrice(Dolboid dolboid)
    {
        return dolboid.CurrentInfo.Mass;
    }
}
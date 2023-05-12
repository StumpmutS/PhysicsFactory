using UnityEngine;

public static class SupplyCalculator
{
    public static int CalculatePrice(Dolboid dolboid)
    {
        return Mathf.Max(1, Mathf.RoundToInt(dolboid.CurrentInfo.Mass));
    }
}
using UnityEngine;

public static class SupplyCalculator
{
    public static float CalculatePrice(Dolboid dolboid)
    {
        return dolboid.CurrentInfo.Mass;
    }
    
    public static float CalculatePrice(float price, Building building, float saleMultiplier = 1)
    {
        return price * building.Info.Volume * saleMultiplier;
    }
    
    public static float CalculatePrice(float price, BuildingPreview preview, float saleMultiplier = 1)
    {
        return price * preview.Volume * saleMultiplier;
    }
}
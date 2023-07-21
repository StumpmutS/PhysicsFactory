using UnityEngine;

public static class SupplyCalculator
{
    public static float CalculatePrice(Dolboid dolboid)
    {
        return dolboid.CurrentInfo.Mass;
    }
    
    public static float CalculatePrice(float price, Building building)
    {
        return price * building.Info.Volume;
    }
    
    public static float CalculatePrice(float price, BuildingPreview preview)
    {
        return price * preview.Volume;
    }
}
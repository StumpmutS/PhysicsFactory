public static class SupplyCalculator
{
    public static float CalculatePrice(Dolboid dolboid)
    {
        return dolboid.CurrentInfo.Mass;
    }
    
    public static float CalculatePrice(float price, Building building, float priceMultiplier = 1)
    {
        return price * building.Info.Volume * priceMultiplier;
    }
    
    public static float CalculatePrice(float price, BuildingPreview preview, float priceMultiplier = 1)
    {
        return price * preview.Volume * priceMultiplier;
    }
}
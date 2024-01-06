public static class SupplyCalculator
{
    public static float CalculatePrice(ResourceData resourceData)
    {
        return ResourceManager.Instance.ModifiedResourceData(resourceData).Amount;
    }
    
    public static float CalculatePrice(float price, Placeable placeable, float priceMultiplier = 1)
    {
        return price * placeable.Data.Volume * priceMultiplier;
    }
    
    public static float CalculatePrice(float price, PlaceablePreview preview, float priceMultiplier = 1)
    {
        return price * preview.Volume * priceMultiplier;
    }
}
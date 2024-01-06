public static class ModificationHelpers
{
    public static PlaceableRestrictionData GenerateRestrictionInfo(Placeable placeable, ModificationData data)
    {
        return new PlaceableRestrictionData(placeable, data.Price, data.SaleMultiplier);
    }
}
public static class ModificationHelpers
{
    public static BuildingRestrictionInfo GenerateRestrictionInfo(Building building, ModificationData data)
    {
        return new BuildingRestrictionInfo(building, data.Price, data.SaleMultiplier);
    }
}
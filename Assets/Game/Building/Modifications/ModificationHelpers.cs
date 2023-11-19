public static class ModificationHelpers
{
    public static BuildingRestrictionInfo GenerateRestrictionInfo(Building building, ModificationInfo info)
    {
        return new BuildingRestrictionInfo(building, info.Price, info.SaleMultiplier);
    }
}
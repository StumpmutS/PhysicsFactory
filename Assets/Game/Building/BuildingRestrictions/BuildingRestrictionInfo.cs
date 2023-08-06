public class BuildingRestrictionInfo
{
    public Building Building;
    public float Price;
    public float SaleMultiplier;

    public BuildingRestrictionInfo(Building building, float price, float saleMultiplier)
    {
        Building = building;
        Price = price;
        SaleMultiplier = saleMultiplier;
    }
}
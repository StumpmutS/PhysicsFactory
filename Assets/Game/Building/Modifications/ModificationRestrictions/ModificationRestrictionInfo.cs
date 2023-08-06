public class ModificationRestrictionInfo
{
    public Building Building;
    public float Price;
    public float SaleMultiplier;

    public ModificationRestrictionInfo(Building building, float price, float saleMultiplier)
    {
        Building = building;
        Price = price;
        SaleMultiplier = saleMultiplier;
    }
}
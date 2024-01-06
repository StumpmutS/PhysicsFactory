public class PlaceableRestrictionData
{
    public Placeable Placeable;
    public float Price;
    public float SaleMultiplier;

    public PlaceableRestrictionData(Placeable placeable, float price, float saleMultiplier)
    {
        Placeable = placeable;
        Price = price;
        SaleMultiplier = saleMultiplier;
    }
}
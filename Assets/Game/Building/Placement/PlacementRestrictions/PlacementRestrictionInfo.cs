using System.Collections.Generic;

public class PlacementRestrictionInfo
{
    public BuildingPreview Preview;
    public float Price;

    public PlacementRestrictionInfo(BuildingPreview preview, float price)
    {
        Preview = preview;
        Price = price;
    }
}
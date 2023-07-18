using System.Collections.Generic;

public class BuildingRestrictionInfo
{
    public BuildingPreview Preview;
    public float Price;

    public BuildingRestrictionInfo(BuildingPreview preview, float price)
    {
        Preview = preview;
        Price = price;
    }
}
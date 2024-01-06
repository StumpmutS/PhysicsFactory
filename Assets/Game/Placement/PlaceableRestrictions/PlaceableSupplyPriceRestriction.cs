using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Placeable/SupplyPrice")]
public class PlaceableSupplyPriceRestriction : Restriction<PlaceableRestrictionData>
{
    protected override ERestrictionFailureType RestrictionFailureType => ERestrictionFailureType.Supply;

    protected override bool Check(PlaceableRestrictionData restrictionData, RestrictionFailureInfo failureInfo)
    {
        var supply = SupplyCalculator.CalculatePrice(restrictionData.Price, restrictionData.Placeable);
        return SupplyManager.Instance.CurrentSupplyCount >= supply;
    }

    public override void PassRestriction(PlaceableRestrictionData data)
    {
        SupplyManager.Instance.SpendSupply(SupplyCalculator.CalculatePrice(data.Price, data.Placeable));
    }
}
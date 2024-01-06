using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Placement/SupplyPrice")]
public class SupplyPriceRestriction : Restriction<PlacementRestrictionInfo>
{
    protected override ERestrictionFailureType RestrictionFailureType => ERestrictionFailureType.Supply;
    
    protected override bool Check(PlacementRestrictionInfo restrictionInfo, RestrictionFailureInfo failureInfo)
    {
        var supply = SupplyCalculator.CalculatePrice(restrictionInfo.Price, restrictionInfo.Preview);
        return SupplyManager.Instance.CurrentSupplyCount >= supply;
    }

    public override void PassRestriction(PlacementRestrictionInfo info)
    {
        SupplyManager.Instance.SpendSupply(SupplyCalculator.CalculatePrice(info.Price, info.Preview));
    }
}
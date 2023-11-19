using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Building/SupplyPrice")]
public class BuildingSupplyPriceRestriction : Restriction<BuildingRestrictionInfo>
{
    protected override ERestrictionFailureType RestrictionFailureType => ERestrictionFailureType.Supply;

    protected override bool Check(BuildingRestrictionInfo restrictionInfo, RestrictionFailureInfo failureInfo)
    {
        var supply = SupplyCalculator.CalculatePrice(restrictionInfo.Price, restrictionInfo.Building);
        failureInfo.Supply = supply;
        return SupplyManager.Instance.CurrentSupplyCount >= supply;
    }

    public override void PassRestriction(BuildingRestrictionInfo info)
    {
        SupplyManager.Instance.SpendSupply(SupplyCalculator.CalculatePrice(info.Price, info.Building));
    }
}
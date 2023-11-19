using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Building/Sale/SupplyPrice")]
public class BuildingSaleSupplyPriceRestriction : Restriction<BuildingRestrictionInfo>
{
    protected override ERestrictionFailureType RestrictionFailureType => ERestrictionFailureType.Supply;

    protected override bool Check(BuildingRestrictionInfo restrictionInfo, RestrictionFailureInfo failureInfo) => true;

    public override void PassRestriction(BuildingRestrictionInfo info)
    {
        SupplyManager.Instance.AddSupply(SupplyCalculator.CalculatePrice(info.Price, info.Building, info.SaleMultiplier));
    }
}
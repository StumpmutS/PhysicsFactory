using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Placement/SupplyPrice")]
public class SupplyPriceRestriction : Restriction<PlacementRestrictionInfo>
{
    public override bool CheckRestriction(PlacementRestrictionInfo info)
    {
        return SupplyManager.Instance.CurrentSupplyCount >= SupplyCalculator.CalculatePrice(info.Price, info.Preview);
    }

    public override void PassRestriction(PlacementRestrictionInfo info)
    {
        SupplyManager.Instance.SpendSupply(SupplyCalculator.CalculatePrice(info.Price, info.Preview));
    }
}
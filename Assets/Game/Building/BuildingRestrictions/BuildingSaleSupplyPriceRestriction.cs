using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Buildings/Sale/SupplyPrice")]
public class BuildingSaleSupplyPriceRestriction : Restriction<BuildingRestrictionInfo>
{
    public override bool CheckRestriction(BuildingRestrictionInfo info) => true;

    public override void PassRestriction(BuildingRestrictionInfo info)
    {
        SupplyManager.Instance.AddSupply(SupplyCalculator.CalculatePrice(info.Price, info.Building, info.SaleMultiplier));
    }
}
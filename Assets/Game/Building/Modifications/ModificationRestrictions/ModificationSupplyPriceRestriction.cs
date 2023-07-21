using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Modifications/SupplyPrice")]
public class ModificationSupplyPriceRestriction : Restriction<ModificationRestrictionInfo>
{
    public override bool CheckRestriction(ModificationRestrictionInfo info)
    {
        return SupplyManager.Instance.CurrentSupplyCount >= SupplyCalculator.CalculatePrice(info.Price, info.Building);
    }

    public override void PassRestriction(ModificationRestrictionInfo info)
    {
        SupplyManager.Instance.SpendSupply(SupplyCalculator.CalculatePrice(info.Price, info.Building));
    }
}
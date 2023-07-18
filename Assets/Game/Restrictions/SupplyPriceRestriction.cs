using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/SupplyPrice")]
public class SupplyPriceRestriction : BuildingRestriction
{
    public override bool CheckRestriction(BuildingRestrictionInfo info)
    {
        return SupplyManager.Instance.CurrentSupplyCount >= info.Price * info.Preview.Volume;
    }

    public override void PassRestriction(BuildingRestrictionInfo info)
    {
        SupplyManager.Instance.SpendSupply(info.Price * info.Preview.Volume);
    }
}
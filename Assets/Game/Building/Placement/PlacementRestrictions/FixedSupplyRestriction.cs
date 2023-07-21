using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Placement/FixedSupply")]
public class FixedSupplyRestriction : Restriction<PlacementRestrictionInfo>
{
    [SerializeField] private float supplyRequired;
    
    public override bool CheckRestriction(PlacementRestrictionInfo info)
    {
        return SupplyManager.Instance.CurrentSupplyCount >= supplyRequired;
    }

    public override void PassRestriction(PlacementRestrictionInfo info)
    {
        SupplyManager.Instance.SpendSupply(supplyRequired);
    }
}
using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/FixedSupply")]
public class FixedSupplyRestriction : BuildingRestriction
{
    [SerializeField] private float supplyRequired;
    
    public override bool CheckRestriction(BuildingRestrictionInfo info)
    {
        return SupplyManager.Instance.CurrentSupplyCount >= supplyRequired;
    }

    public override void PassRestriction(BuildingRestrictionInfo info)
    {
        SupplyManager.Instance.SpendSupply(supplyRequired);
    }
}
using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Supply")]
public class SupplyRestriction : BuildingRestriction
{
    [SerializeField] private int supplyRequired;
    
    public override bool CheckRestriction(BuildingRestrictionInfo info)
    {
        return SupplyManager.Instance.CurrentSupplyCount >= supplyRequired;
    }

    public override void PassRestriction(BuildingRestrictionInfo info)
    {
        SupplyManager.Instance.SpendSupply(supplyRequired);
    }
}
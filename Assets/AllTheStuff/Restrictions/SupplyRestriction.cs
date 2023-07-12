using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Supply")]
public class SupplyRestriction : BuildingRestriction
{
    [SerializeField] private float supplyRequired;
    
    public override bool CheckRestriction(BuildingRestrictionInfo info)
    {
        return SupplyManager.Instance.CurrentSupplyCount >= supplyRequired * info.Preview.Volume;
    }

    public override void PassRestriction(BuildingRestrictionInfo info)
    {
        SupplyManager.Instance.SpendSupply(supplyRequired * info.Preview.Volume);
    }
}
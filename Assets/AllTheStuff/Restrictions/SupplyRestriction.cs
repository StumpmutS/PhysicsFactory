using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Supply")]
public class SupplyRestriction : Restriction
{
    [SerializeField] private int supplyRequired;
    
    public override bool CheckRestriction()
    {
        return SupplyManager.Instance.CurrentSupplyCount >= supplyRequired;
    }

    public override void PassRestriction()
    {
        SupplyManager.Instance.SpendSupply(supplyRequired);
    }
}
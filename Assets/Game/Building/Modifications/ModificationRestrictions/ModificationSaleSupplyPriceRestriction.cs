using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Modifications/Sale/SupplyPrice")]
public class ModificationSaleSupplyPriceRestriction : Restriction<ModificationRestrictionInfo>
{
    [SerializeField, Range(0, 1)] private float saleMultiplier = 1;
    
    public override bool CheckRestriction(ModificationRestrictionInfo info) => true;

    public override void PassRestriction(ModificationRestrictionInfo info)
    {
        SupplyManager.Instance.AddSupply(SupplyCalculator.CalculatePrice(info.Price, info.Building) * saleMultiplier);
    }
}
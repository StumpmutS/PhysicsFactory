using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Buildings/Sale/SupplyPrice")]
public class BuildingSaleSupplyPriceRestriction : Restriction<BuildingRestrictionInfo>
{
    [SerializeField, Range(0, 1)] private float saleMultiplier = 1;
    
    public override bool CheckRestriction(BuildingRestrictionInfo info) => true;

    public override void PassRestriction(BuildingRestrictionInfo info)
    {
        SupplyManager.Instance.AddSupply(SupplyCalculator.CalculatePrice(info.Price, info.Building) * saleMultiplier);
    }
}
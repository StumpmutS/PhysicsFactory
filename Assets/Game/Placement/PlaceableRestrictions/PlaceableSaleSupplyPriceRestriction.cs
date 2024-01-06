using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Placeable/Sale/SupplyPrice")]
public class PlaceableSaleSupplyPriceRestriction : Restriction<PlaceableRestrictionData>
{
    protected override ERestrictionFailureType RestrictionFailureType => ERestrictionFailureType.Supply;

    protected override bool Check(PlaceableRestrictionData restrictionData, RestrictionFailureInfo failureInfo) => true;

    public override void PassRestriction(PlaceableRestrictionData data)
    {
        SupplyManager.Instance.AddSupply(SupplyCalculator.CalculatePrice(data.Price, data.Placeable, data.SaleMultiplier));
    }
}
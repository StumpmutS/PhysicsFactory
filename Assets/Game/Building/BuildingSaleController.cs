using UnityEngine;
using UnityEngine.Events;

public class BuildingSaleController : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    [SerializeField] private Building building;

    public string SaleText =>
        $"Sell {building.Info.Label}: ${SupplyCalculator.CalculatePrice(building.Info.Price, building, building.Info.SaleMultiplier):F2}";

    public UnityEvent OnSale;

    public void Sell()
    {
        if (!RestrictionHelper.TryPassRestrictions(building.Info.SaleRestrictions,
                new BuildingRestrictionInfo(building, building.Info.Price, building.Info.SaleMultiplier),
                new RestrictionFailureInfo())) return;
        OnSale.Invoke();
        Destroy(parent);
    }
}
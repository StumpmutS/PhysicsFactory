using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BuildingSaleController : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    [SerializeField] private Building building;

    public string SaleText =>
        $"Sell {building.Data.Label}: ${SupplyCalculator.CalculatePrice(building.Data.Price, building, building.Data.SaleMultiplier):F2}";

    public UnityEvent OnSale;

    public void Sell()
    {
        if (!RestrictionHelper.TryPassRestrictions(building.Data.SaleRestrictionRefs.Select(c => c.Data),
                new BuildingRestrictionInfo(building, building.Data.Price, building.Data.SaleMultiplier),
                new RestrictionFailureInfo())) return;
        OnSale.Invoke();
        Destroy(parent);
    }
}
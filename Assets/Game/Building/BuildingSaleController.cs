using System;
using UnityEngine;
using UnityEngine.Events;

public class BuildingSaleController : MonoBehaviour
{
    [SerializeField] private Building building;

    public string SaleText =>
        $"Sell {building.Info.Label}: ${SupplyCalculator.CalculatePrice(building.Info.Price, building):F2}";

    public UnityEvent OnSale;

    public void Sell()
    {
        if (!RestrictionHelper.TryPassRestrictions(building.Info.SaleRestrictions,
                new BuildingRestrictionInfo(building, building.Info.Price))) return;
        OnSale.Invoke();
        Destroy(gameObject);
    }
}
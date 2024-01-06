using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SaleController : MonoBehaviour
{
    [SerializeField] private GameObject parent;

    public float SalePriceSummation => _sellables.Sum(s => s.SalePrice);
    
    private HashSet<ISellable> _sellables = new();

    public UnityEvent OnSale = new();

    private void Awake()
    {
        _sellables = parent.GetComponentsInChildren<ISellable>().ToHashSet();
    }

    public void Sell()
    {
        SupplyManager.Instance.AddSupply(SalePriceSummation);

        OnSale.Invoke();
        Destroy(parent);
    }
}
using UnityEngine;

public class EnergyStorageSummaryService : DataService<string>
{
    [SerializeField] private EnergyStorage storage;
    
    public override string RequestData()
    {
        return $"Charge: {storage.TotalCharge:F2}";
    }
}
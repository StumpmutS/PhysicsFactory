using UnityEngine;

public class MultiplierChargeController : ChargeController
{
    [SerializeField] private float multiplier;
    
    protected override float CalculateCharge(float charge)
    {
        return charge * multiplier;
    }
}
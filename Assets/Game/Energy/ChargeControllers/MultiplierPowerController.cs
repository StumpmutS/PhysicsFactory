using UnityEngine;

public class MultiplierPowerController : PowerController
{
    [SerializeField] private float multiplier;
    
    protected override float CalculatePower(float charge)
    {
        return charge * multiplier;
    }
}
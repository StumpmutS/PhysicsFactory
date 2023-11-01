using UnityEngine;

public class ExponentialReciprocalPowerController : PowerController
{
    [SerializeField, Tooltip("1.07177 for 1 power at 10 charge")] private float exponentBase = 1.07177f;
    
    protected override float CalculatePower(float charge)
    {
        var resultingPower = Mathf.Pow(exponentBase, charge) - 1;
        if (resultingPower <= 0)
        {
            return 0;
        }
        return 1 / resultingPower;
    }
}
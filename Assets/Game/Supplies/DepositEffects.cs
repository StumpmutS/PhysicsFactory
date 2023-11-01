using System;
using UnityEngine;

[Serializable]
public class DepositEffects
{
    [SerializeField] private float multiplier;
    public float Multiplier => multiplier;

    public DepositEffects(float multiplier = 1f)
    {
        this.multiplier = multiplier;
    }
}
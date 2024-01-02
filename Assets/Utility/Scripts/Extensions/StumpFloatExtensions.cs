using System;
using UnityEngine;

namespace Utility.Scripts.Extensions
{
    public static class StumpFloatExtensions
    {
        public static float Truncate(this float value, int decimalPlaces)
        {
            var epsilon = Mathf.Pow(10, -decimalPlaces - 2);
            value += epsilon;
            
            if (decimalPlaces < 0)
            {
                Debug.LogError("Cannot truncate negative number of decimal places");
                return value;
            }

            var integralValue = (int) value;
            var decimalValue = value - integralValue;
            var placeMultiplier = Mathf.Pow(10, decimalPlaces);
            var truncatedDecimalValue = ((int) (decimalValue * placeMultiplier)) / placeMultiplier;
            
            return integralValue + truncatedDecimalValue;
        }
    }
}
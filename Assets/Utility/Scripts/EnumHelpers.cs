using System;
using System.Collections.Generic;

public static class EnumHelpers
{
    public static IEnumerable<T> GetFlaggedValues<T>(this T @enum) where T : Enum
    {
        var flaggedBits = Convert.ToInt32(@enum);
        foreach (Enum value in Enum.GetValues(@enum.GetType()))
        {
            var bitFlag = Convert.ToInt32(value);
            if (bitFlag == 0 && flaggedBits != 0) continue;
            if ((bitFlag & flaggedBits) == bitFlag) yield return (T) value;
        }
    }
}

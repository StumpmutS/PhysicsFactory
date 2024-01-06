using System;
using System.Collections.Generic;
using System.Linq;

public static class StumpEnumExtensions
{
    public static IEnumerable<T> GetFlaggedValues<T>(this T @enum) where T : Enum
    {
        var flaggedBits = Convert.ToInt32(@enum);
        if (flaggedBits == 0)
        {
            yield return default;
            yield break;
        }
        
        foreach (Enum value in Enum.GetValues(@enum.GetType()))
        {
            var bitFlag = Convert.ToInt32(value);
            if (bitFlag == 0) continue;
            
            if ((bitFlag & flaggedBits) == bitFlag) yield return (T) value;
        }
    }

    public static T AsSingleFlag<T>(this T @enum) where T : Enum
    {
        return @enum.GetFlaggedValues().FirstOrDefault();
    }
}

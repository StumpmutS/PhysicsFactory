using System.Collections.Generic;
using System.Linq;

public static class EnergyConversionHelpers
{
    public static float ConvertEnergy(IEnumerable<EnergyConverter> converters, float charge)
    {
        return converters.Aggregate(charge, (current, converter) => converter.ConvertEnergy(current));
    }

    public static float UnconvertEnergy(IEnumerable<EnergyConverter> converters, float charge)
    {
        return converters.Reverse()
            .Aggregate(charge,
                (current, converter) => converter.UnconvertEnergy(current));
    }
}
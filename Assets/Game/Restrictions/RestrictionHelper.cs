using System.Collections.Generic;
using System.Linq;

public static class RestrictionHelper
{
    public static bool CheckRestrictions<T>(IEnumerable<Restriction<T>> restrictions, T info)
    {
        return restrictions.All(restriction => restriction.CheckRestriction(info));
    }
    
    public static void PassRestrictions<T>(IEnumerable<Restriction<T>> restrictions, T info)
    {
        foreach (var restriction in restrictions)
        {
            restriction.PassRestriction(info);
        }
    }

    public static bool TryPassRestrictions<T>(IEnumerable<Restriction<T>> restrictions, T info)
    {
        var restrictionList = restrictions.ToList();
        var value = CheckRestrictions(restrictionList, info);
        if (value) PassRestrictions(restrictionList, info);
        return value;
    }
}
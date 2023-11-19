using System.Collections.Generic;
using System.Linq;

public static class RestrictionHelper
{
    public static bool CheckRestrictions<T>(IEnumerable<Restriction<T>> restrictions, T info, RestrictionFailureInfo failureInfo)
    {
        bool passed = true;
        foreach (var restriction in restrictions)
        {
            if (!restriction.CheckRestriction(info, failureInfo)) passed = false;
        }

        return passed;
    }
    
    public static void PassRestrictions<T>(IEnumerable<Restriction<T>> restrictions, T info)
    {
        foreach (var restriction in restrictions)
        {
            restriction.PassRestriction(info);
        }
    }

    public static bool TryPassRestrictions<T>(IEnumerable<Restriction<T>> restrictions, T info, RestrictionFailureInfo failureInfo)
    {
        var restrictionList = restrictions.ToList();
        var passed = CheckRestrictions(restrictionList, info, failureInfo);
        if (passed) PassRestrictions(restrictionList, info);
        return passed;
    }
}
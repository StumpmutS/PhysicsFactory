using System.Collections.Generic;
using System.Linq;

public static class RestrictionHelper
{
    public static bool CheckRestrictions(IEnumerable<Restriction> restrictions)
    {
        return restrictions.All(restriction => restriction.CheckRestriction());
    }
    
    public static void PassRestrictions(IEnumerable<Restriction> restrictions)
    {
        foreach (var restriction in restrictions)
        {
            restriction.PassRestriction();
        }
    }

    public static bool TryPassRestrictions(IEnumerable<Restriction> restrictions)
    {
        var restrictionList = restrictions.ToList();
        var value = CheckRestrictions(restrictionList);
        if (value) PassRestrictions(restrictionList);
        return value;
    }
}
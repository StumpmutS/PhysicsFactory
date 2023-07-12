using System.Collections.Generic;
using System.Linq;

public static class RestrictionHelper
{
    public static bool CheckRestrictions(IEnumerable<BuildingRestriction> restrictions, BuildingRestrictionInfo info)
    {
        return restrictions.All(restriction => restriction.CheckRestriction(info));
    }
    
    public static void PassRestrictions(IEnumerable<BuildingRestriction> restrictions, BuildingRestrictionInfo info)
    {
        foreach (var restriction in restrictions)
        {
            restriction.PassRestriction(info);
        }
    }

    public static bool TryPassRestrictions(IEnumerable<BuildingRestriction> restrictions, BuildingRestrictionInfo info)
    {
        var restrictionList = restrictions.ToList();
        var value = CheckRestrictions(restrictionList, info);
        if (value) PassRestrictions(restrictionList, info);
        return value;
    }
}
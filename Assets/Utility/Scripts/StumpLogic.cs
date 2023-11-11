using System;
using System.Collections.Generic;
using System.Linq;

namespace Utility.Scripts
{
    public static class StumpLogic
    {
        //if at least desiredTrue many conditions are true return true
        public static bool AtLeastOr(IEnumerable<bool> conditions, int desiredTrue)
        {
            return conditions.Sum(Convert.ToInt32) >= desiredTrue;
        }
    }
}
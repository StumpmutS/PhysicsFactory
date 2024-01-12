using System.Collections.Generic;
using System.Reflection;

namespace Utility.Editor
{
    public static class ReflectionHelpers
    {
        public static FieldInfo FindField(int maxRecursions, HashSet<object> targets, string name, out object newTarget)
        {
            newTarget = null;
            if (maxRecursions < 1) return null;
        
            var foundTargets = new HashSet<object>();
        
            foreach (var target in targets)
            {
                if (target == null) continue;
            
                var type = target.GetType();
                while (type != null)
                {
                    foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic |
                                                         BindingFlags.Instance))
                    {
                        if (field.Name == name)
                        {
                            newTarget = target;
                            return field;
                        }

                        foundTargets.Add(field.GetValue(target));
                    }

                    type = type.BaseType;
                }
            }

            maxRecursions--;
            return FindField(maxRecursions, foundTargets, name, out newTarget);
        }
    }
}
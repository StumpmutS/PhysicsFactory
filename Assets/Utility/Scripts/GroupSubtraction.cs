using System.Collections.Generic;
using System.Linq;

namespace Utility.Scripts
{
    public class GroupSubtraction
    {
        public static List<KeyValuePair<T, SignedFloat>> DistributedSubtract<T>(Dictionary<T, SignedFloat> floats, float targetSubtractAmount)
        {
            var orderedFloats = new Stack<KeyValuePair<T, SignedFloat>>(floats.OrderBy(kvp => kvp.Value.Value));
            var subtractGroup = new List<KeyValuePair<T, SignedFloat>> { orderedFloats.Pop() };
            var subtracted = 0f;

            while (subtracted + .001f < targetSubtractAmount)
            {
                var nextFloat = orderedFloats.Count > 0 ? orderedFloats.Peek().Value.Value : 0;
                var highestPossibleDiff = (subtractGroup[0].Value.Value - nextFloat) * subtractGroup.Count; //how much can be subtracted in current group
                var desiredDiff = targetSubtractAmount - subtracted; //how much needs to be subtracted in current group
                if (highestPossibleDiff >= desiredDiff)
                {
                    GroupSubtract(subtractGroup, desiredDiff);
                    subtracted += desiredDiff;
                }
                else
                {
                    GroupSubtract(subtractGroup, highestPossibleDiff);
                    subtractGroup.Add(orderedFloats.Pop());
                    subtracted += highestPossibleDiff;
                }
            }

            return subtractGroup;
        }

        private static void GroupSubtract<T>(List<KeyValuePair<T, SignedFloat>> subtractGroup, float amount)
        {
            for (int i = 0; i < subtractGroup.Count; i++)
            {
                var newValue = subtractGroup[i].Value.Value - amount / subtractGroup.Count;
                subtractGroup[i] = new KeyValuePair<T, SignedFloat>(subtractGroup[i].Key, new SignedFloat(newValue, subtractGroup[i].Value.Positive)); 
            }
        }
    }
}
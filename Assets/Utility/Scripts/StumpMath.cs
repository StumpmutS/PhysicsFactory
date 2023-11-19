namespace Utility.Scripts
{
    public static class StumpMath
    {
        public static int MoveTowards(int value, int target, int maxDelta)
        {
            if (value < target)
            {
                value += maxDelta;
                if (value > target) return target;
                return value;
            }
        
            if (value > target)
            {
                value -= maxDelta;
                if (value < target) return target;
                return value;
            }

            return target;
        }
    }
}
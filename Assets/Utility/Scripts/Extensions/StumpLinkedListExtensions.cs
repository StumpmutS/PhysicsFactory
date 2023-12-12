using System.Collections.Generic;

namespace Utility.Scripts.Extensions
{
    public static class StumpLinkedListExtensions
    {
        public static bool TryPopLast<T>(this LinkedList<T> linkedList, out T value)
        {
            value = default;
            if (linkedList.Count <= 0) return false;
            
            value = linkedList.Last.Value;
            linkedList.RemoveLast();
            return true;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Utility.Scripts.Extensions
{
    public static class StumpDictionaryExtensions
    {
        public static TKey MinKeyByValue<TKey>(this IEnumerable<KeyValuePair<TKey, float>> dictionary)
        {
            TKey key = default;
            var shortest = Mathf.Infinity;
            foreach (var kvp in dictionary)
            {
                if (!(kvp.Value < shortest)) continue;
                shortest = kvp.Value;
                key = kvp.Key;
            }

            return key;
        }
        
        public static TKey MinKeyByValue<TKey>(this IEnumerable<KeyValuePair<TKey, int>> dictionary)
        {
            TKey key = default;
            var shortest = Mathf.Infinity;
            foreach (var kvp in dictionary)
            {
                if (!(kvp.Value < shortest)) continue;
                shortest = kvp.Value;
                key = kvp.Key;
            }

            return key;
        }
        
        public static TKey MaxKeyByValue<TKey>(this IEnumerable<KeyValuePair<TKey, float>> dictionary)
        {
            TKey key = default;
            var longest = -Mathf.Infinity;
            foreach (var kvp in dictionary)
            {
                if (kvp.Value < longest) continue;
                longest = kvp.Value;
                key = kvp.Key;
            }

            return key;
        }
        
        public static TKey MaxKeyByValue<TKey>(this IEnumerable<KeyValuePair<TKey, int>> dictionary)
        {
            TKey key = default;
            var longest = -Mathf.Infinity;
            foreach (var kvp in dictionary)
            {
                if (kvp.Value < longest) continue;
                longest = kvp.Value;
                key = kvp.Key;
            }

            return key;
        }

        //Assumes distinct values
        public static bool TryGetKeyFromValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, TValue value, out TKey key)
        {
            key = default;
            
            foreach (var kvp in dictionary)
            {
                if (!kvp.Value.Equals(value)) continue;
                
                key = kvp.Key;
                return true;
            }

            return false;
        }
    }
}

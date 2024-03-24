using System.Collections.Generic;
using UnityEngine;

namespace Utility.Scripts
{
    public class StumpRandom
    {
        /// <summary>
        /// Generates an amount of random unique numbers in given range.
        /// Works better the larger the range is compared to the amount
        /// </summary>
        /// <param name="minInclusive">Range min inclusive</param>
        /// <param name="maxExclusive">Range max exclusive</param>
        /// <param name="amount">Amount of numbers to generate</param>
        /// <returns>Generated numbers</returns>
        public static IEnumerable<int> FastSample(int minInclusive, int maxExclusive, int amount)
        {
            if (maxExclusive - minInclusive < amount)
            {
                Debug.LogError("Range is not large enough to generate requested number of distinct values");
                return null;
            }
            
            var numbers = new HashSet<int>();
            while (numbers.Count < amount)
            {
                numbers.Add(Random.Range(minInclusive, maxExclusive));
            }
            
            return numbers;
        }
        
        /// <summary>
        /// Generates an amount of random unique numbers in given range
        /// </summary>
        /// <param name="minInclusive">Range min inclusive</param>
        /// <param name="maxExclusive">Range max exclusive</param>
        /// <param name="amount">Amount of numbers to generate</param>
        /// <returns>Generated numbers</returns>
        public static IEnumerable<int> SafeSample(int minInclusive, int maxExclusive, int amount)
        {
            if (maxExclusive - minInclusive < amount)
            {
                Debug.LogError("Range is not large enough to generate requested number of distinct values");
                return null;
            }

            var available = new List<int>(maxExclusive - minInclusive);
            for (int i = 0; i < available.Count; i++)
            {
                available[i] = i + minInclusive;
            }

            var numbers = new HashSet<int>();
            for (int i = 0; i < amount; i++)
            {
                int determinedIndex = Random.Range(0, available.Count);
                numbers.Add(available[determinedIndex]);
                available.Remove(determinedIndex);
            }
            
            return numbers;
        }
        
        public static Vector3Int DeterminePrismSingleEdgeOpening(Vector3Int dimensions)
        {
            var result = new Vector3Int(Random.Range(1, dimensions.x - 1), Random.Range(1, dimensions.y - 1), Random.Range(1, dimensions.z - 1));
            var edge = Random.Range(0, 6);
            switch (edge)
            {
                case 0: //left
                    result.x = 0;
                    break;
                case 1: //right
                    result.x = dimensions.x - 1;
                    break;
                case 2: //down
                    result.y = 0;
                    break;
                case 3: //up
                    result.y = dimensions.y - 1;
                    break;
                case 4: //back
                    result.z = 0;
                    break;
                case 5: //forward
                    result.z = dimensions.z - 1;
                    break;
                default:
                    Debug.LogError("Uh oh!");
                    return Vector3Int.zero;
            }

            return result;
        }
    }
}
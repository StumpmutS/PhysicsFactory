using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility.Scripts
{
    public static class StumpListExtensions
    {
        public static void Populate<T>(this List<List<T>> grid, T populateWith, int mainCount, int subCount)
        {
            while (grid.Count < mainCount)
            {
                grid.Add(new List<T>());
            }
            foreach (var list in grid)
            {
                while (list.Count < subCount)
                {
                    list.Add(populateWith);
                }
            }
        }
        
        public static void Populate<T>(this List<List<T>> grid, Func<T> populateWith, int mainCount, int subCount)
        {
            if (populateWith == null)
            {
                Debug.LogError("populateWith function is null");
                return;
            }
            
            while (grid.Count < mainCount)
            {
                grid.Add(new List<T>());
            }
            foreach (var list in grid)
            {
                while (list.Count < subCount)
                {
                    list.Add(populateWith.Invoke());
                }
            }
        }
        
        public static void Equalize<T>(this List<T> list, int targetSize)
        {
            if (list.Count > targetSize)
            {
                list.RemoveRange(targetSize, list.Count - targetSize);
            }

            while (list.Count < targetSize)
            {
                list.Add(default);
            }
        }
    }
}

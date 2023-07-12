using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility.Scripts
{
    public class IntegerGroup<T>
    {
        private int _maxTotal;
        public int MaxTotal
        {
            get => _maxTotal;
            set
            {
                var prevTotal = _maxTotal;
                _maxTotal = value;
                if (prevTotal >= value) return;

                while (Total > _maxTotal)
                {
                    var integersCopy = Integers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                    
                    foreach (var kvp in Integers)
                    {
                        var kvpValue = kvp.Value;
                        if (kvpValue.Value <= 0) continue;
                        kvpValue.Value--;
                        integersCopy[kvp.Key] = kvpValue;
                        if (Total == _maxTotal) break;
                    }

                    Integers = integersCopy;
                }
                
                OnIntegersChanged.Invoke();
            }
        }
        public long Total => Integers.Values.Sum(i => i.Value);
        public Dictionary<T, SignedInt> Integers { get; private set; } = new();

        public event Action OnIntegersChanged = delegate { };
        
        public void SetValue(T key, int value)
        {
            if (!Integers.ContainsKey(key)) Integers[key] = new SignedInt(0, true);
            
            var available = MaxTotal - Total;
            var difference = Mathf.Abs(value) - Integers[key].Value;
            if (available < difference)
            {
                Integers[key] = new SignedInt((uint)Mathf.Abs(Integers[key].Value + available), value >= 0);
            }
            else Integers[key] = new SignedInt((uint)Mathf.Abs(value), value >= 0);
            
            OnIntegersChanged.Invoke();
        }
    }
}
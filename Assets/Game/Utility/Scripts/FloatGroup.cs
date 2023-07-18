using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility.Scripts
{
    public class FloatGroup<T>
    {
        private float _maxTotal;
        public float MaxTotal
        {
            get => _maxTotal;
            set
            {
                if (value < 0) return;
                var diff = Total - value; //how much needs to be subtracted in total
                _maxTotal = value;
                if (diff <= 0) return;
                var replacedFloats = GroupSubtraction.DistributedSubtract(Floats, diff);
                foreach (var kvp in replacedFloats)
                {
                    Floats[kvp.Key] = kvp.Value;
                }
                OnFloatsChanged.Invoke();
            }
        }
        public float Total => Floats.Values.Sum(i => i.Value);
        public Dictionary<T, SignedFloat> Floats { get; private set; } = new();

        public event Action OnFloatsChanged = delegate { };
        
        public void SetValue(T key, SignedFloat value)
        {
            if (!Floats.ContainsKey(key)) Floats[key] = new SignedFloat(0, true);
            
            var available = MaxTotal - Total;
            var difference = Mathf.Abs(value.Value) - Floats[key].Value;
            if (available < difference)
            {
                Floats[key] = new SignedFloat(Floats[key].Value + available, value.Positive);
            }
            else Floats[key] = value;
            
            OnFloatsChanged.Invoke();
        }
    }
}
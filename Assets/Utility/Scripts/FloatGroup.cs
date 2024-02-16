using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility.Scripts
{
    public class FloatGroup<T> where T : class, IFloatGroupKey
    {
        private float _maxTotal;
        public float MaxTotal
        {
            get => _maxTotal;
            set
            {
                if (value < 0) return;
                
                _maxTotal = value;
                
                var diff = CurrentTotal - value; // How much needs to be subtracted in total
                if (diff <= 0) return;
                var replacedFloats =
                    GroupSubtraction.DistributedSubtract(FloatKeys.ToDictionary(f => f, f => f.EntryData.FloatGroupValue), diff);
                foreach (var kvp in replacedFloats)
                {
                    kvp.Key.EntryData = new FloatGroupEntryData(kvp.Value, Available);
                }
            }
        }
        public float CurrentTotal => FloatKeys.Sum(k => k.EntryData.FloatGroupValue.Value);
        public HashSet<T> FloatKeys { get; private set; } = new();

        private float Available => MaxTotal - CurrentTotal;
        
        /// <summary>
        /// Sets or adds the key value pair
        /// </summary>
        public void SetValue(T key, SignedFloat value)
        {
            FloatKeys.Add(key);
            
            var difference = Mathf.Abs(value.Value) - key.EntryData.FloatGroupValue.Value;
            float newAvailable = 0f;
            if (Available < difference)
            {
                key.EntryData =
                    new FloatGroupEntryData(
                        new SignedFloat(key.EntryData.FloatGroupValue.Value + Available, value.Positive), newAvailable);
            }
            else
            {
                newAvailable = Available - difference;
                key.EntryData = new FloatGroupEntryData(value, newAvailable);
            }
            
            UpdateAvailability(newAvailable);
        }

        private void UpdateAvailability(float availability)
        {
            foreach (var floatKey in FloatKeys)
            {
                floatKey.EntryData = new FloatGroupEntryData(floatKey.EntryData.FloatGroupValue, availability);
            }
        }

        public void Remove(T key)
        {
            FloatKeys.Remove(key);
            UpdateAvailability(Available);
        }
    }
}
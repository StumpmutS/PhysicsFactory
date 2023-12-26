using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Utility.Scripts
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [FormerlySerializedAs("_keys")] [SerializeField] private List<TKey> keys = new();
        [FormerlySerializedAs("_values")] [SerializeField] private List<TValue> values = new();

        private bool _valid;

        public void ForceUpdate()
        {
            Validate();
            OnBeforeSerialize();
        }
        
        public void OnBeforeSerialize()
        {
            if (!_valid) return;
            
            keys.Clear();
            values.Clear();

            keys = Keys.ToList();
            values = Values.ToList();
        }

        public void OnAfterDeserialize()
        {
            Validate();
            if (!_valid) return;
            
            Clear();

            for (int i = 0; i < keys.Count; i++)
            {
                TryAdd(keys[i], values.Count <= i ? default : values[i]);
            }
        }

        private void Validate()
        {
            if (keys.Count != keys.Distinct().Count())
            {
                Debug.LogError("Dictionary has duplicate keys");
                _valid = false;
                return;
            }

            _valid = true;
        }
    }
}

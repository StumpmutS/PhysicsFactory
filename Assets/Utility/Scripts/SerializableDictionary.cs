using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility.Scripts
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<TKey> _keys = new();
        [SerializeField] private List<TValue> _values = new();

        private bool _valid;
        
        public void OnBeforeSerialize()
        {
            if (!_valid) return;
            
            _keys.Clear();
            _values.Clear();

            _keys = Keys.ToList();
            _values = Values.ToList();
        }

        public void OnAfterDeserialize()
        {
            Validate();
            if (!_valid) return;
            
            Clear();

            for (int i = 0; i < _keys.Count; i++)
            {
                TryAdd(_keys[i], _values.Count <= i ? default : _values[i]);
            }
        }

        private void Validate()
        {
            if (_keys.Count != _keys.Distinct().Count())
            {
                Debug.LogError("Dictionary has duplicate keys");
                _valid = false;
                return;
            }

            _valid = true;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Utility.Scripts
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [FormerlySerializedAs("_keys")] [SerializeField] private List<TKey> keys = new();
        [FormerlySerializedAs("_values")] [SerializeField] private List<TValue> values = new();

        private Dictionary<TKey, TValue> _dictionary = new();
        private bool _valid;
        private bool Valid
        {
            get => _valid;
            set
            {
                if (_valid == value) return;
                if (!value) Debug.LogError("Dictionary has duplicate keys");
                _valid = value;
            }
        }

        public TValue this[TKey key]
        {
            get => _dictionary[key];
            set
            {
                _dictionary[key] = value;
                OnBeforeSerialize();
            }
        }

        public SerializableDictionary() { }

        public SerializableDictionary(IDictionary<TKey, TValue> dict)
        {
            _dictionary = new Dictionary<TKey, TValue>(dict);
            OnBeforeSerialize();
        }

        public void OnBeforeSerialize()
        {
            Validate();
            if (!Valid) return;
            
            keys.Clear();
            values.Clear();

            keys = Keys.ToList();
            values = Values.ToList();
        }

        public void OnAfterDeserialize()
        {
            Validate();
            if (!Valid) return;
            
            Clear();

            for (int i = 0; i < keys.Count; i++)
            {
                _dictionary.TryAdd(keys[i], values.Count <= i ? default : values[i]);
            }
        }

        private void Validate()
        {
            if (keys.Count != keys.Distinct().Count())
            {
                Valid = false;
                return;
            }

            Valid = true;
        }

        #region IDictionary Implementation
        public int Count => _dictionary.Count;
        public bool IsReadOnly => false;
        public ICollection<TKey> Keys => _dictionary.Keys;
        public ICollection<TValue> Values => _dictionary.Values;
        
        public void Add(TKey key, TValue value) => _dictionary.Add(key, value);

        public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);

        public bool Remove(TKey key) => _dictionary.Remove(key);

        public bool TryGetValue(TKey key, out TValue value) => _dictionary.TryGetValue(key, out value);

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(KeyValuePair<TKey, TValue> item) => _dictionary.Add(item.Key, item.Value);

        public void Clear() => _dictionary.Clear();

        public bool Contains(KeyValuePair<TKey, TValue> item) =>
            _dictionary.TryGetValue(item.Key, out var value) && value.Equals(item.Value);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => throw new NotImplementedException();

        public bool Remove(KeyValuePair<TKey, TValue> item) => Contains(item) && _dictionary.Remove(item.Key);
        #endregion
    }
}

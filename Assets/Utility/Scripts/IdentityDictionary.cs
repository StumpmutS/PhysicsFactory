using System;
using System.Collections;
using System.Collections.Generic;

public class IdentityDictionary<TKey, TValue, TID> : IEnumerable
{
    public int Count => _dictionary.Count;
    public bool IsReadOnly => false;
    public ICollection<TKey> Keys => _dictionary.Keys;
    public ICollection<Tuple<TValue, TID>> Values
    {
        get
        {
            var values = new List<Tuple<TValue, TID>>(Count);
            foreach (var key in Keys)
            {
                values.Add(new Tuple<TValue, TID>(_dictionary[key], _identities[key]));
            }

            return values;
        }
    }
    public Tuple<TValue, TID> this[TKey key]
    {
        get => new Tuple<TValue, TID>(_dictionary[key], _identities[key]);
        set
        {
            _identities[key] = value.Item2;
            _dictionary[key] = value.Item1;
        }
    }
    
    private Dictionary<TKey, TID> _identities = new();
    private Dictionary<TKey, TValue> _dictionary = new();
    
    public IEnumerator<KeyValuePair<TKey, Tuple<TValue, TID>>> GetEnumerator()
    {
        return new IdentityDictionaryEnum<TKey, TValue, TID>(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(KeyValuePair<TKey, Tuple<TValue, TID>> item)
    {
        _identities.Add(item.Key, item.Value.Item2);
    }

    public void Clear()
    {
        _identities.Clear();
        _dictionary.Clear();
    }
    
    public void Add(TKey key, Tuple<TValue, TID> value)
    {
        _identities.Add(key, value.Item2);
        _dictionary.Add(key, value.Item1);
    }

    public bool ContainsKey(TKey key)
    {
        return _identities.ContainsKey(key) && _dictionary.ContainsKey(key);
    }

    public bool Remove(TKey key, TID id)
    {
        if (!_identities.TryGetValue(key, out var foundId) || !foundId.Equals(id)) return false;
        
        return _dictionary.Remove(key) && _identities.Remove(key);
    }

    public bool TryGetValue(TKey key, TID id, out TValue value)
    {
        value = default;

        if (!_identities.TryGetValue(key, out var foundId) || !foundId.Equals(id)) return false;

        return _dictionary.TryGetValue(key, out value);
    }
}
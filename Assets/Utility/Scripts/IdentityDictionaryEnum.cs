using System;
using System.Collections;
using System.Collections.Generic;

public class IdentityDictionaryEnum<TKey, TValue, TID> : IEnumerator<KeyValuePair<TKey, Tuple<TValue, TID>>>
{
    public KeyValuePair<TKey, Tuple<TValue, TID>> Current
    {
        get
        {
            var key = (TKey) _keyEnumerator.Current;
            var value = _dictionary[key];
            return new KeyValuePair<TKey, Tuple<TValue, TID>>(key, value);
        }
    }
    object IEnumerator.Current => Current;
    
    private IdentityDictionary<TKey, TValue, TID> _dictionary;
    private IEnumerator _keyEnumerator;
    
    public IdentityDictionaryEnum(IdentityDictionary<TKey, TValue, TID> dictionary)
    {
        _dictionary = dictionary;
        _keyEnumerator = _dictionary.Keys.GetEnumerator();
    }
    
    public bool MoveNext()
    {
        return _keyEnumerator.MoveNext();
    }

    public void Reset()
    {
        _keyEnumerator.Reset();
    }
    
    public void Dispose()
    {
        if (_keyEnumerator is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}
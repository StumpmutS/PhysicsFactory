using System;
using UnityEngine;

public abstract class Vector3SpringListener : SpringListener
{
    [SerializeField, ShowIf(nameof(useSetValue), true, 3)]
    private Vector3 minValue, origValue, maxValue;

    private Vector3 _origValue;

    private void Awake()
    {
        _origValue = useSetValue ? origValue : GetOrig();
        if (useSetValue) return;

        minValue = _origValue * minMultiplier;
        maxValue = _origValue * maxMultiplier;
    }

    protected abstract Vector3 GetOrig();

    public override void HandleSpringValue(float amount, float target)
    {
        switch (amount)
        {
            case > 0:
                ChangeValue(_origValue + (maxValue - _origValue) * amount);
                break;
            case < 0:
                ChangeValue(_origValue + (_origValue - minValue) * amount);
                break;
            case 0:
                ChangeValue(_origValue);
                break;
        }
    }

    protected abstract void ChangeValue(Vector3 value);
}
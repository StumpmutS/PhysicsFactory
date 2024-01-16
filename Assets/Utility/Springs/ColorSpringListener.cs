using System;
using UnityEngine;

public abstract class ColorSpringListener : SpringListener
{
    [SerializeField, ShowIf(nameof(useSetValue), true, 3)]
    private ColorSelectionWrapper startingMinValue;
    [SerializeField, ShowIf(nameof(useSetValue), true, 3)]
    private ColorSelectionWrapper startingOrigValue;
    [SerializeField, ShowIf(nameof(useSetValue), true, 3)]
    private ColorSelectionWrapper startingMaxValue;

    private Color _minValue, _origValue, _maxValue;
    
    private void Awake()
    {
        if (useSetValue)
        {
            _minValue = startingMinValue.Color; 
            _origValue = startingOrigValue.Color;
            _maxValue = startingMaxValue.Color;
        }
        else
        {
            _origValue = GetOrig();
            UpdateValues();
        }
    }

    private void UpdateValues()
    {
        if (useSetValue) return;

        _minValue = _origValue * minMultiplier;
        _maxValue = _origValue * maxMultiplier;
    }

    protected abstract Color GetOrig();

    public void SetOrig(Color color)
    {
        _origValue = color;
        UpdateValues();
    }

    protected override void HandleSpringValue(float amount, float target)
    {
        switch (amount)
        {
            case > 0:
                ChangeValue(_origValue + (_maxValue - _origValue) * amount);
                break;
            case < 0:
                ChangeValue(_origValue + (_origValue - _minValue) * amount);
                break;
            default:
                ChangeValue(_origValue);
                break;
        }
    }

    protected abstract void ChangeValue(Color value);

    public void SetMaxColor(Color color)
    {
        _maxValue = color;
    }
}

[Serializable]
public class IntWrapper
{
    public int inte;
}
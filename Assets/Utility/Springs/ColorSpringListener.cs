using UnityEngine;

public abstract class ColorSpringListener : SpringListener
{
    [SerializeField, ShowIf(nameof(useSetValue), true)] private Color minValue, origValue, maxValue;
    
    private Color _origValue;
    
    private void Awake()
    {
        _origValue = useSetValue ? origValue : GetOrig();
        UpdateValues();
    }

    private void UpdateValues()
    {
        if (useSetValue) return;

        minValue = _origValue * minMultiplier;
        maxValue = _origValue * maxMultiplier;
    }

    protected abstract Color GetOrig();

    public void SetOrig(Color color)
    {
        _origValue = color;
        UpdateValues();
    }

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
            default:
                ChangeValue(_origValue);
                break;
        }
    }

    protected abstract void ChangeValue(Color value);

    public void SetMaxColor(Color color)
    {
        maxValue = color;
    }
}

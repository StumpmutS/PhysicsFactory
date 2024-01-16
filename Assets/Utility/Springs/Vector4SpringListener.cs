using UnityEngine;

public abstract class Vector4SpringListener : SpringListener
{
    [SerializeField, ShowIf(nameof(useSetValue), true, 7)] private Vector4 minValue, origValue, maxValue;
    
    private Vector4 _origValue;
    
    private void Start()
    {
        _origValue = useSetValue ? origValue : GetOrig();
        if (useSetValue) return;
        
        minValue = _origValue * minMultiplier;
        maxValue = _origValue * maxMultiplier;
    }

    protected abstract Vector4 GetOrig();

    protected override void HandleSpringValue(float amount, float target)
    {
        switch (amount)
        {
            case > 0:
                ChangeValue(_origValue + (maxValue - _origValue) * amount);
                break;
            case < 0:
                ChangeValue(_origValue + (_origValue - minValue) * amount);
                break;
        }
    }

    protected abstract void ChangeValue(Vector4 value);
}
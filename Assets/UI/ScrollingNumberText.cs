using System;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingNumberText : MonoBehaviour
{
    [SerializeField] private LayoutDisplay layout;
    [SerializeField] private ScrollingIntegerDigit scrollingDigitPrefab;
    [SerializeField] private RectTransform decimalPrefab;
    [SerializeField] private int decimalPlaces;
    
    private LinkedList<ScrollingIntegerDigit> _digits = new();
    
    private void Awake()
    {
        for (int i = 0; i < decimalPlaces; i++)
        {
            AddDigit();
        }

        layout.Add(Instantiate(decimalPrefab));
        AddDigit();
    }

    public void SetValue(float value)
    {
        int decimalAdjustedValue = Mathf.CeilToInt(Mathf.Abs(value) * Mathf.Pow(10, decimalPlaces));
        var current = _digits.Last;
        int index = 0;
        while (true)
        {
            current ??= AddDigit();
            current.Value.SetTarget(decimalAdjustedValue);
            decimalAdjustedValue /= 10;
            index++;
            if (decimalAdjustedValue == 0 && index > decimalPlaces + 1) break;
            current = current.Previous;
        }
        
        while (current.Previous != null)
        {
            TryRemoveDigit();
        }
    }

    private LinkedListNode<ScrollingIntegerDigit> AddDigit()
    {
        var digit = Instantiate(scrollingDigitPrefab);
        if (digit.transform is not RectTransform rectTransform)
        {
            Debug.LogError("Digit's transform was not a rect transform");
            return null;
        }
        layout.Add(rectTransform);
        return _digits.AddFirst(digit);
    }

    private void TryRemoveDigit()
    {
        if (_digits.Count < decimalPlaces + 1) return;
        Destroy(_digits.First.Value.gameObject);
        _digits.RemoveFirst();
    }
}

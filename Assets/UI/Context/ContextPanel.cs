using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContextPanel : MonoBehaviour
{
    [SerializeField] private LayoutDisplay layout;
    [SerializeField] private TMP_Text label;
    [SerializeField] private RectTransform divider;
    [SerializeField] private TMP_Text summaryText;
    [SerializeField] private KeyCodeCombinationDisplay keyCodeCombinationDisplayPrefab;
    
    public void Display(ContextData data)
    {
        layout.Clear();
        
        divider.gameObject.SetActive(!string.IsNullOrEmpty(data.Label));
        var labels = data.ContextReferences.Select(s => s.RequestData()).ToArray();
        label.text = string.Format(data.Label, labels);
        summaryText.text = string.Format(data.Summary, labels);

        foreach (var combinationData in data.KeyCombinations)
        {
            layout.AddPrefab(keyCodeCombinationDisplayPrefab, k => k.Display(combinationData));
        }
    }
}
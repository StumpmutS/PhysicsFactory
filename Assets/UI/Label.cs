using System;
using TMPro;
using UnityEngine;
using Utility.Scripts;
using Utility.Scripts.Extensions;

public class Label : MonoBehaviour, IInitializableComponent<TMP_Text>
{
    [SerializeField] private TMP_Text text;
    
    public void Init(TMP_Text textPrefab)
    {
        if (text == null) text = Instantiate(textPrefab, transform, false);
    }

    public void SetLabel(string value)
    {
        text.text = value;
    }

    public static void SetLabel(Component component, string value)
    {
        var label = component.AddOrGetComponent<Label>();
        label.SetLabel(value);
    }
}
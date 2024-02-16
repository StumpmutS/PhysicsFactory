using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ContextData
{
    public string Label;
    [TextArea] public string Summary;
    public List<KeyCodeCombinationData> KeyCombinations;
    public List<DataService<string>> ContextReferences;

    public event Action OnUpdated = delegate { };
    
    public ContextData(string label = "", string summary = "", List<KeyCodeCombinationData> keyCombinations = null, List<DataService<string>> contextReferences = null)
    {
        Label = label;
        Summary = summary;
        KeyCombinations = keyCombinations;
        ContextReferences = contextReferences;
    }

    private void HandleUpdate(string _)
    {
        OnUpdated.Invoke();
    }
}

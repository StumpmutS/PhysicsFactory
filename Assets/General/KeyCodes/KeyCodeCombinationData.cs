using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KeyCodeCombinationData
{
    [SerializeField] private List<KeyCodeData> keyCodeCombination;
    public List<KeyCodeData> KeyCodeCombination => keyCodeCombination;
    [SerializeField] private string description;
    public string Description => description;
}

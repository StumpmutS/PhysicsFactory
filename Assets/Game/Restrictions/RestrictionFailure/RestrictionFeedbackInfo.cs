using System;
using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;

[Serializable]
public class RestrictionFeedbackInfo
{
    [SerializeField] private string text;
    public string Text => text;
    [SerializeField] private IconData icon;
    public IconData Icon => icon;
    [SerializeField] private List<ComponentAdder> componentAdders;
    public List<ComponentAdder> ComponentAdders => componentAdders;
}
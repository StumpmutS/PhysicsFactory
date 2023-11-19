using System;
using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;

[Serializable]
public class RestrictionFeedbackInfo
{
    [SerializeField] private string text;
    public string Text => text;
    [SerializeField] private IconInfo icon;
    public IconInfo Icon => icon;
    [SerializeField] private List<ComponentAdder> componentAdders;
    public List<ComponentAdder> ComponentAdders => componentAdders;
}
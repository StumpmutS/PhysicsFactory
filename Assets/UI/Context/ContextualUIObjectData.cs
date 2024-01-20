using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class ContextualUIObjectData
{
    [SerializeField] private bool hoverDisplay;
    public bool HoverDisplay => hoverDisplay;
    [SerializeField, ShowIf(nameof(hoverDisplay), true, 3)] private FloatSelectionWrapper hoverTime;
    public float HoverTime => hoverTime.Value;
    [FormerlySerializedAs("driveLabel")] [SerializeField] private bool labelDriven;
    public bool LabelDriven => labelDriven;
    [SerializeField] private bool summaryDriven;
    public bool SummaryDriven => summaryDriven;
    [SerializeField] private string labelSummarySeparator;
    public string LabelSummarySeparator => labelSummarySeparator;

    public ContextualUIObjectData(bool hoverDisplay, bool labelDriven)
    {
        this.hoverDisplay = hoverDisplay;
        this.labelDriven = labelDriven;
    }
}
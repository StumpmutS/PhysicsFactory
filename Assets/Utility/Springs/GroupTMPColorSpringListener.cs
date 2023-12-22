using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GroupTMPColorSpringListener : ColorSpringListener
{
    [SerializeField] private List<TMP_Text> texts;
    
    protected override Color GetOrig()
    {
        return texts.First().color;
    }

    protected override void ChangeValue(Color value)
    {
        foreach (var text in texts)
        {
            text.color = value;
        }
    }
}
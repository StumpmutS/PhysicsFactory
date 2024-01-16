using System.Collections.Generic;
using UnityEngine;
using UI = UnityEngine.UI;

public class GroupSelectableSpringListener : SpringListener, ISerializationCallbackReceiver
{
    [SerializeField] private bool allowAtNeg1, allowAt0, allowAt1;
    [SerializeField] private List<UI.Selectable> selectables;

    protected override void HandleSpringValue(float amount, float target)
    {
        bool allowed = DetermineAllowance(target);
        foreach (var selectable in selectables)
        {
            selectable.interactable = allowed;
        }
    }

    private bool DetermineAllowance(float target)
    {
        return target switch
        {
            < -.001f => allowAtNeg1,
            > .001f => allowAt1,
            _ => allowAt0
        };
    }

    public void OnBeforeSerialize()
    {
        useSetValue = true;
    }

    public void OnAfterDeserialize()
    {
        useSetValue = true;
    }
}

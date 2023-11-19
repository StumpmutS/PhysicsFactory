using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GroupTextAlphaSpringListener : FloatSpringListener, ISerializationCallbackReceiver
{
    [SerializeField] private ERangeContainment rangeContainType;
    [SerializeField] private List<TMP_Text> texts;

    protected override float GetOrig()
    {
        return origValue;
    }

    protected override void ChangeValue(float value, float _)
    {
        foreach (var text in texts)
        {
            var color = text.color;
            color.a = rangeContainType == ERangeContainment.ClampZero ? ClampZero(value) : Mathf.Abs(value);
            text.color = color;
        }
    }

    private float ClampZero(float value)
    {
        if (value < 0) value = 0;
        return value;
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
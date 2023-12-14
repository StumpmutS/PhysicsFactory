using System;
using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;

public class TimeScaleManager : Singleton<TimeScaleManager>
{
    [SerializeField] private float defaultTimeScale = 1f;
    
    private LinkedList<object> _scaleSetters = new();
    private Dictionary<object, float> _setterValues = new();

    public void SetTimeScale(object setter, float value)
    {
        _scaleSetters.AddLast(setter);
        _setterValues.Add(setter, value);
        RefreshTimeScale();
    }

    public void ResetTimeScale(object setter)
    {
        _scaleSetters.Remove(setter);
        _setterValues.Remove(setter);
        RefreshTimeScale();
    }

    private void RefreshTimeScale()
    {
        if (_scaleSetters.Count < 1 || _scaleSetters.Count != _setterValues.Count)
        {
            Time.timeScale = defaultTimeScale;
            return;
        }

        Time.timeScale = _setterValues[_scaleSetters.Last.Value];
    }
}
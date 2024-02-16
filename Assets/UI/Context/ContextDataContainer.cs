using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContextDataContainer : DataService<ContextData>
{
    [SerializeField] private ContextData startingData;
    
    private ContextData _data;
    private List<DataService<string>> _dataReferences = new();

    private void Awake()
    {
        SetData(startingData);
    }
    
    public override ContextData RequestData() => _data;
    
    public void SetData(ContextData contextData)
    {
        if (_data == contextData) return;

        StopListeningToData();
        _data = contextData;
        if (_data.ContextReferences != null) _dataReferences = _data.ContextReferences.ToList();
        ListenToData();
        
        OnUpdated.Invoke(_data);
    }

    private void ListenToData()
    {
        _data.OnUpdated += HandleDataUpdate;
        
        foreach (var reference in _dataReferences)
        {
            reference.OnUpdated.AddListener(HandleReferenceUpdate);
        }
    }
    
    private void StopListeningToData()
    {
        if (_data != null) _data.OnUpdated -= HandleDataUpdate;

        foreach (var reference in _dataReferences)
        {
            if (reference != null) reference.OnUpdated.RemoveListener(HandleReferenceUpdate);
        }
    }

    private void HandleDataUpdate()
    {
        OnUpdated.Invoke(_data);
    }

    private void HandleReferenceUpdate(string _)
    {
        OnUpdated.Invoke(_data);
    }

    private void OnDestroy()
    {
        StopListeningToData();
    }
}

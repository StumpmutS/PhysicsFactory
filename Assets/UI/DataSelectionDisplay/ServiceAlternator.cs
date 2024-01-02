using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceAlternator<TData> : DataService<TData>
{
    [SerializeField] private DataService<TData> defaultService;

    private DataService<TData> _currentService;

    private void Awake()
    {
        SetService(defaultService);
    }

    public void SetService(Component component)
    {
        if (component is not DataService<TData> service)
        {
            Debug.LogError($"{component} component is not a service of type {typeof(TData)}");
            return;
        }
        
        _currentService = service;
    }
    
    public override IEnumerable<TData> RequestData()
    {
        return _currentService.RequestData();
    }
}
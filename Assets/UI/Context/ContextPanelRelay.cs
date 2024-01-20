using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ContextPanelRelay : MonoBehaviour
{
    [SerializeField] private DataService<ContextData> contextService;

    protected virtual void Awake()
    {
        if (contextService == null) contextService = GetComponent<DataService<ContextData>>();
    }

    public void Display()
    {
        HandleDisplay(ContextPanelManager.Instance.DisplayFloatingPanel(this, contextService.RequestData()));
    }

    protected virtual void HandleDisplay(ContextPanel panel) { }
    
    public void RemoveDisplay()
    {
        ContextPanelManager.Instance.TryRemoveFloatingDisplay(this);
    }
}

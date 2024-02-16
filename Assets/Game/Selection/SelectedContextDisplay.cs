public class SelectedContextDisplay : SelectableDisplay<DataService<ContextData>>
{
    private DataService<ContextData> _service;
    
    protected override void SetupSelectionDisplay(DataService<ContextData> contextService)
    {
        _service = contextService;
        ContextPanelManager.Instance.DisplayLockedPanel(this, _service.RequestData());
        _service.OnUpdated.AddListener(HandleContextUpdate);
    }

    private void HandleContextUpdate(ContextData contextData)
    {
        ContextPanelManager.Instance.DisplayLockedPanel(this, contextData);
    }

    protected override void RemoveSelectionDisplay()
    {
        if (_service != null) _service.OnUpdated.RemoveListener(HandleContextUpdate);
        ContextPanelManager.Instance.RemoveLockedDisplay(this);
    }
}
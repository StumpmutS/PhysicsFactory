public class SelectedContextDisplay : SelectableDisplay<DataService<ContextData>>
{
    protected override void SetupSelectionDisplay(DataService<ContextData> contextService)
    {
        ContextPanelManager.Instance.DisplayLockedPanel(this, contextService.RequestData());
    }

    protected override void RemoveSelectionDisplay()
    {
        ContextPanelManager.Instance.RemoveLockedDisplay(this);
    }
}
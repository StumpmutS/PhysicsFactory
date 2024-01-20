public class ContextLabelService : ContextDrivenService
{
    protected override void HandleUpdate(ContextData context)
    {
        OnUpdated.Invoke(context.Label);
    }

    public override string RequestData()
    {
        return contextService.RequestData().Label;
    }
}
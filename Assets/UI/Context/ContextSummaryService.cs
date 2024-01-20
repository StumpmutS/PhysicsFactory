public class ContextSummaryService : ContextDrivenService
{
    public override string RequestData()
    {
        return contextService.RequestData().Summary;
    }

    protected override void HandleUpdate(ContextData context)
    {
        OnUpdated.Invoke(context.Summary);
    }
}
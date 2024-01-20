using UnityEngine;

public class ContextLabelSummaryService : ContextDrivenService
{
    [SerializeField] private string separator;

    public void Init(string separator)
    {
        this.separator = separator;
    }
    
    public override string RequestData()
    {
        return GenerateStringData(contextService.RequestData());
    }

    protected override void HandleUpdate(ContextData context)
    {
        OnUpdated.Invoke(GenerateStringData(context));
    }

    private string GenerateStringData(ContextData data)
    {
        return $"{data.Label}{separator}{data.Summary}";
    }
}
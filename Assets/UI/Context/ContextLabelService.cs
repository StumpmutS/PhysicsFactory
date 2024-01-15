using UnityEngine;

public class ContextLabelService : DataService<string>
{
    [SerializeField] private DataService<ContextData> contextService;

    private void Awake()
    {
        contextService.OnUpdated.AddListener(HandleUpdate);
    }

    private void HandleUpdate(ContextData context)
    {
        OnUpdated.Invoke(context.Label);
    }

    public override string RequestData()
    {
        return contextService.RequestData().Label;
    }

    private void OnDestroy()
    {
        if (contextService != null) contextService.OnUpdated.RemoveListener(HandleUpdate);
    }
}
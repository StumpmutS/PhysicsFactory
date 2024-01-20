using UnityEngine;

public abstract class ContextDrivenService : DataService<string>
{
    [SerializeField] protected DataService<ContextData> contextService;

    private void Awake()
    {
        if (contextService == null) contextService = GetComponent<DataService<ContextData>>();
        contextService.OnUpdated.AddListener(HandleUpdate);
    }

    protected abstract void HandleUpdate(ContextData context);

    private void OnDestroy()
    {
        if (contextService != null) contextService.OnUpdated.RemoveListener(HandleUpdate);
    }
}
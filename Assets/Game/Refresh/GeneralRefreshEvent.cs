using UnityEngine;
using UnityEngine.Events;

public class GeneralRefreshEvent : MonoBehaviour
{
    private IRefreshable[] _refreshables;
    
    public UnityEvent OnRefresh = new();
    
    private void Awake()
    {
        _refreshables = GetComponents<IRefreshable>();
        foreach (var refreshable in _refreshables)
        {
            refreshable.OnRefresh += HandleRefresh;
        }
    }

    private void HandleRefresh()
    {
        OnRefresh.Invoke();
    }

    private void OnDestroy()
    {
        foreach (var refreshable in _refreshables)
        {
            if (refreshable != null) refreshable.OnRefresh -= HandleRefresh;
        }
    }
}
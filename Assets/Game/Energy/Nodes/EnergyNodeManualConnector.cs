using UnityEngine;

public class EnergyNodeManualConnector : MonoBehaviour
{
    [SerializeField] private EnergyNode node;
    [SerializeField] private EnergyNodeAutoConnector autoConnector;
    
    private bool _active;
    
    public void TryActivate()
    {
        if (_active) return;
        _active = true;
        
        SelectionEvents.Instance.OnEngaged.AddListener(HandleEngaged);
    }
    
    public void TryDeactivate()
    {
        if (!_active) return;
        _active = false;
        
        SelectionEvents.Instance.OnEngaged.RemoveListener(HandleEngaged);
    }

    private void HandleEngaged(Selectable selectable)
    {
        if (!selectable.MainObject.TryGetComponent<EnergyNode>(out var foundNode)) return;
        if (!AttemptConnectAction(foundNode)) return;
        
        autoConnector.Locked = true;
        SelectionManager.Instance.DisengageAll();
    }

    private bool AttemptConnectAction(EnergyNode other)
    {
        return node.TryDisconnect(other) || node.TryConnect(other);
    }
}
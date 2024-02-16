using System.Collections.Generic;
using UnityEngine;

public class PacketConnectionVisualizer : MonoBehaviour
{
    [SerializeField] private EnergyNode energyNode;
    [SerializeField] private GameObject currentVisualsPrefab;

    private HashSet<GameObject> _currentVisuals = new();
    private bool _active;

    public void TryActivate()
    {
        if (_active) return;
        
        energyNode.OnConnectionsUpdated.AddListener(Refresh);
        Activate();
    }

    private void Activate()
    {
        _active = true;
        foreach (var connection in energyNode.PacketConnections)
        {
            VisualizeConnection(connection);
        }
    }
    
    private void Refresh()
    {
        if (TryDeactivate()) Activate();
    }
    
    public bool TryDeactivate()
    {
        if (!_active) return false;
        
        energyNode.OnConnectionsUpdated.RemoveListener(Refresh);
        Deactivate();
        return true;
    }

    private void Deactivate()
    {
        _active = false;
        foreach (var current in _currentVisuals)
        {
            Destroy(current.gameObject);
        }
        _currentVisuals.Clear();
    }

    private void VisualizeConnection(ChargePacketConnection connection)
    {
        var origin = connection.Sender.transform.position;
        var destination = ((Component) connection.Receiver).transform.position;
        var direction = destination - origin;
        var visuals = Instantiate(currentVisualsPrefab);
        visuals.transform.forward = direction;
        visuals.transform.position = Vector3.Lerp(origin, destination, .5f);
        visuals.transform.localScale = new Vector3(visuals.transform.localScale.x, visuals.transform.localScale.y,
            direction.magnitude);
        
        _currentVisuals.Add(visuals);
    }
}

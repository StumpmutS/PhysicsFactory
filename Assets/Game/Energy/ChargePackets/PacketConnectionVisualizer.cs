using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PacketConnectionVisualizer : MonoBehaviour
{
    [SerializeField] private EnergyNode energyNode;
    [FormerlySerializedAs("currentVisualsPrefab")] [SerializeField] private PacketConnectionVisual visualsPrefab;

    private HashSet<PacketConnectionVisual> _visuals = new();
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
        TryDeactivate();
        TryActivate();
    }
    
    public void TryDeactivate()
    {
        if (!_active) return;
        
        energyNode.OnConnectionsUpdated.RemoveListener(Refresh);
        Deactivate();
    }

    private void Deactivate()
    {
        _active = false;
        foreach (var visual in _visuals)
        {
            if (visual != null) Destroy(visual.gameObject);
        }
        _visuals.Clear();
    }

    private void VisualizeConnection(ChargePacketConnection connection)
    {
        if (!gameObject.scene.isLoaded) return;
        
        var origin = connection.Sender.transform.position;
        var destination = ((Component) connection.Receiver).transform.position;
        var direction = destination - origin;
        var visuals = Instantiate(visualsPrefab);
        visuals.transform.forward = direction;
        visuals.transform.position = origin;
        
        var main = visuals.ParticleSystem.main;
        main.startLifetime =
            new ParticleSystem.MinMaxCurve(
                direction.magnitude / visuals.ParticleSystem.velocityOverLifetime.y.constantMax);
        visuals.ParticleSystem.Play();
        
        _visuals.Add(visuals);
    }
}

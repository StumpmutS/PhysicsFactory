using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnergyNodeAutoConnector : MonoBehaviour
{
    [SerializeField] private EnergyNodeFinder nodeFinder;
    [SerializeField] private EnergyNode node;

    private bool _locked;
    public bool Locked
    {
        get => _locked;
        set
        {
            if (value == _locked) return;
            _locked = value;
            OnLockChanged.Invoke();
            RefreshNodes(nodeFinder.Nodes);
        }
    }
    
    private HashSet<EnergyNode> _connectedNodes = new();

    public UnityEvent OnLockChanged;

    private void Awake()
    {
        nodeFinder.OnRangeUpdated.AddListener(_ => RefreshNodes(nodeFinder.Nodes));
    }

    private void Start()
    {
        PlaceableManager.Instance.OnPlaceableAdded.AddListener(HandlePlaceableAdded);
        RefreshNodes(nodeFinder.Nodes);
    }

    private void HandlePlaceableAdded()
    {
        RefreshNodes(nodeFinder.Nodes);
    }

    private void RefreshNodes(List<EnergyNode> nodes)
    {
        if (Locked) return;
        
        foreach (var connectedNode in _connectedNodes.ToList())
        {
            if (nodes.Contains(connectedNode)) continue;
            node.TryDisconnect(connectedNode);
            _connectedNodes.Remove(connectedNode);
        }
        ConnectNodes(nodes);
    }

    private void ConnectNodes(List<EnergyNode> nodes)
    {
        if (LevelDataHandler.Instance == null || !LevelDataHandler.Instance.Loaded) return;

        foreach (var other in nodes)
        {
            if (node.TryConnect(other)) _connectedNodes.Add(other);
        }
    }

    private void OnDestroy()
    {
        if (PlaceableManager.Instance != null) PlaceableManager.Instance.OnPlaceableAdded.RemoveListener(HandlePlaceableAdded);
    }
}

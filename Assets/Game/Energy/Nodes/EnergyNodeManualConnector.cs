﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Scripts;

public class EnergyNodeManualConnector : Singleton<EnergyNodeManualConnector>
{
    private HashSet<EnergyNode> _nodes = new();

    public void HandleEngaged(Selectable selectable)
    {
        if (!selectable.MainObject.TryGetComponent<EnergyNode>(out var node)) return;
        selectable.OnDisengage.AddListener(HandleDisengaged);

        if (AttemptConnection(node))
        {
            SelectionManager.Instance.DisengageAll();
            ClearNodes();
            return;
        }

        AddNode(node);
    }

    private void HandleDisengaged(Selectable selectable)
    {
        selectable.OnDisengage.RemoveListener(HandleDisengaged);
        if (!selectable.MainObject.TryGetComponent<EnergyNode>(out var node)) return;
        RemoveNode(node);
    }

    private bool AttemptConnection(EnergyNode node)
    {
        bool connected = false;
        foreach (var other in _nodes)
        {
            if (node.TryConnect(other)) connected = true;
        }

        return connected;
    }

    private void AddNode(EnergyNode node)
    {
        _nodes.Add(node);
    }

    private void RemoveNode(EnergyNode node)
    {
        _nodes.Remove(node);
    }

    private void ClearNodes()
    {
        _nodes.Clear();
    }
}
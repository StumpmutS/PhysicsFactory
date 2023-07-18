using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;

public class EnergyNodeConnector : Singleton<EnergyNodeConnector>
{
    private HashSet<EnergyContainerNode> _containerNodes = new();
    private HashSet<EnergyGeneratorNode> _generatorNodes = new();

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
        return node switch
        {
            EnergyContainerNode containerNode => AttemptContainerConnection(containerNode),
            EnergyGeneratorNode generatorNode => AttemptGeneratorConnection(generatorNode),
            _ => false
        };
    }
    
    private bool AttemptContainerConnection(EnergyContainerNode node)
    {
        foreach (var generatorNode in _generatorNodes)
        {
            node.InitiateCurrent(generatorNode.Generator, node.Container, generatorNode);
        }

        return _generatorNodes.Count > 0;
    }

    private bool AttemptGeneratorConnection(EnergyGeneratorNode node)
    {
        foreach (var containerNode in _containerNodes)
        {
            node.InitiateCurrent(node.Generator, containerNode.Container, node);
        }

        return _containerNodes.Count > 0;
    }

    private void AddNode(EnergyNode node)
    {
        switch (node)
        {
            case EnergyContainerNode containerNode:
                _containerNodes.Add(containerNode);
                break;
            case EnergyGeneratorNode generatorNode:
                _generatorNodes.Add(generatorNode);
                break;
        }
    }

    private void RemoveNode(EnergyNode node)
    {
        switch (node)
        {
            case EnergyContainerNode containerNode:
                _containerNodes.Remove(containerNode);
                break;
            case EnergyGeneratorNode generatorNode:
                _generatorNodes.Remove(generatorNode);
                break;
        }
    }

    private void ClearNodes()
    {
        _containerNodes.Clear();
        _generatorNodes.Clear();
    }
}
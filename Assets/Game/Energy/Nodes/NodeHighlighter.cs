using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodeHighlighter : MonoBehaviour
{
    [SerializeField] private ENodeType targetNodeType; 
    [SerializeField] private EnergyNodeFinder finder;

    private HashSet<Highlightable> _highlighted = new();
    
    public void Activate()
    {
        Deactivate();
        HighlightNodes(finder.Nodes);
    }

    public void Deactivate()
    {
        HighlightManager.Instance.UnHighlight(this, _highlighted);
        _highlighted.Clear();
    }

    private void HighlightNodes(List<EnergyNode> nodes)
    {
        var highlightables = nodes.Where(n => n.NodeType == targetNodeType)
            .Select(n => n.GetComponent<Highlightable>())
            .Where(h => h != null);
        _highlighted = highlightables.ToHashSet();
        HighlightManager.Instance.Highlight(this, _highlighted);
    }
}
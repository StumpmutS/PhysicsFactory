using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Scripts;

public class NodeHighlighter : Activatable
{
    [SerializeField] private TypeReference nodeType;
    [SerializeField] private EnergyNodeFinder finder;

    private HashSet<Highlightable> _highlighted = new();
    
    protected override void HandleActivation()
    {
        HighlightNodes(finder.Nodes);
    }
    
    protected override void HandleDeactivation()
    {
        HighlightManager.Instance.UnHighlight(this, _highlighted);
        _highlighted.Clear();
    }

    private void HighlightNodes(List<EnergyNode> nodes)
    {
        var highlightables = nodes.Where(n => n.GetType() == nodeType.TargetType)
            .Select(n => n.GetComponent<Highlightable>())
            .Where(h => h != null);
        HighlightManager.Instance.UnHighlight(this, _highlighted.Where(h => !highlightables.Contains(h)));
        _highlighted = highlightables.ToHashSet();
        HighlightManager.Instance.Highlight(this, _highlighted);
    }
}
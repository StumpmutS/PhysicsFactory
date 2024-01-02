using System.Collections.Generic;
using Utility.Scripts;

public class HighlightManager : Singleton<HighlightManager>
{
    private Dictionary<Highlightable, HashSet<object>> _highlightables = new();

    public void AddHighlightable(Highlightable highlightable)
    {
        _highlightables.Add(highlightable, new HashSet<object>());
    }

    public void RemoveHighlightable(Highlightable highlightable)
    {
        _highlightables.Remove(highlightable);
    }

    public void Highlight(object caller, IEnumerable<Highlightable> highlightables)
    {
        print(caller);
        foreach (var highlightable in highlightables)
        {
            _highlightables[highlightable].Add(caller);
            highlightable.ActivateHighlight();
        }
    }

    public void UnHighlight(object caller, IEnumerable<Highlightable> highlightables)
    {
        foreach (var highlightable in highlightables)
        {
            if (!_highlightables.ContainsKey(highlightable)) continue;
            
            _highlightables[highlightable].Remove(caller);
            if (_highlightables[highlightable].Count < 1) highlightable.DeactivateHighlight();
        }
    }
}
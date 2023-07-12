using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Scripts;

public class SelectionManager : Singleton<SelectionManager>
{
    [SerializeField] private float maxSearchDistance = 200f;
    [SerializeField] private LayerMask selectableLayer;
    
    private Selectable _hovered;
    private Selectable _selected;
    private HashSet<Selectable> _engaged = new();
    private HashSet<Selectable> _prioritizedSelectables = new();
    
    public void PrioritizeSelectables(IEnumerable<Selectable> selectables)
    {
        _prioritizedSelectables = selectables.ToHashSet();
    }
    
    public void UnPrioritizeSelectables(IEnumerable<Selectable> selectables)
    {
        foreach (var selectable in selectables)
        {
            _prioritizedSelectables.Remove(selectable);
        }
    }
    
    private void Update()
    {
        UpdateHovered();
    }

    private void UpdateHovered()
    {
        var results = new RaycastHit[128];
        Physics.RaycastNonAlloc(MainCameraRef.Cam.ScreenPointToRay(Input.mousePosition), results, maxSearchDistance, selectableLayer);

        foreach (var hit in results)
        {
            if (hit.collider == null) break;

            if (!hit.collider.TryGetComponent<Selectable>(out var selectable) || !_prioritizedSelectables.Contains(selectable)) continue;
            
            SetHovered(selectable);
            return;
        }

        if (results[0].collider != null && results[0].collider.TryGetComponent<Selectable>(out var firstSelectable))
        {
            SetHovered(firstSelectable);
        }
        else SetHovered(null);
    }

    private void SetHovered(Selectable selectable)
    {
        if (_hovered != null) _hovered.StopHover();
        _hovered = selectable;
        if (selectable == null) return;
        selectable.Hover();
    }

    public void SelectHovered()
    {
        if (_selected != null) _selected.Deselect();
        if (_hovered != null) _hovered.Select();
        _selected = _hovered;
    }

    public void EngageHovered()
    {
        if (_hovered != null)
        {
            _hovered.Engage();
            _engaged.Add(_hovered);
            return;
        }
        
        DisengageAll();
    }

    public void DisengageAll()
    {
        foreach (var selectable in _engaged)
        {
            selectable.Disengage();
        }
        
        _engaged.Clear();
    }
}
using System;
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
        results = results.Where(hit => hit.collider != null)
            .OrderBy(hit => Vector3.SqrMagnitude(hit.point - MainCameraRef.Cam.transform.position)).ToArray();
        if (results.Length == 0)
        {
            SetHovered(null);
            return;
        }

        foreach (var hit in results)
        {
            if (!hit.collider.TryGetComponent<Selectable>(out var selectable) || !_prioritizedSelectables.Contains(selectable)) continue;
            
            SetHovered(selectable);
            return;
        }

        SetHovered(results[0].collider.TryGetComponent<Selectable>(out var firstSelectable) ? firstSelectable : null);
    }

    private void SetHovered(Selectable selectable)
    {
        if (selectable == _hovered) return;
        if (_hovered != null) _hovered.StopHover();
        _hovered = selectable;
        if (selectable == null) return;
        selectable.Hover();
    }

    public void SelectHovered()
    {
        if (_selected != null)
        {
            _selected.OnDeselect.RemoveListener(HandleEarlyDeselect);
            _selected.Deselect();
        }
        if (_hovered != null)
        {
            _hovered.Select(); 
            _hovered.OnDeselect.AddListener(HandleEarlyDeselect);
        }
        _selected = _hovered;
    }

    private void HandleEarlyDeselect(Selectable selectable)
    {
        selectable.OnDeselect.RemoveListener(HandleEarlyDeselect);
        if (_selected == selectable) _selected = null;
    }

    public void EngageHovered()
    {
        if (_hovered != null)
        {
            _engaged.Add(_hovered);
            _hovered.Engage();
            _hovered.OnDisengage.AddListener(HandleEarlyDisengage);
            return;
        }
        
        DisengageAll();
    }

    private void HandleEarlyDisengage(Selectable selectable)
    {
        selectable.OnDisengage.RemoveListener(HandleEarlyDisengage);
        _engaged.Remove(selectable);
    }

    private bool _disengageAll;
    public void DisengageAll() => _disengageAll = true;

    private void LateUpdate()
    {
        if (!_disengageAll) return;
        _disengageAll = false;
        
        foreach (var selectable in _engaged)
        {
            selectable.OnDisengage.RemoveListener(HandleEarlyDisengage);
            selectable.Disengage();
        }
        
        _engaged.Clear();
    }
}
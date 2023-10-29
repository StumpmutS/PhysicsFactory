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
    private bool _disabled;
    private HashSet<object> _disablers = new();

    public void Enable(object caller)
    {
        _disablers.Remove(caller);
        if (_disablers.Count < 1) _disabled = false;
    }

    public void Disable(object caller)
    {
        _disabled = true;
        _disablers.Add(caller);
        TryStopHover();
        TryDeselect();
        DisengageAll();
    }
    
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

    private readonly RaycastHit[] _raycastResults = new RaycastHit[128];
    
    private void UpdateHovered()
    {
        for (int i = 0; i < _raycastResults.Length; i++)
        {
            _raycastResults[i] = default;
        }
        
        Physics.RaycastNonAlloc(MainCameraRef.Cam.ScreenPointToRay(Input.mousePosition), _raycastResults, maxSearchDistance, selectableLayer);
        var filteredResults = _raycastResults.Where(hit => hit.collider != null)
            .OrderBy(hit => Vector3.SqrMagnitude(hit.point - MainCameraRef.Cam.transform.position)).ToArray();
        if (filteredResults.Length == 0)
        {
            SetHovered(null);
            return;
        }

        foreach (var hit in filteredResults)
        {
            if (!hit.collider.TryGetComponent<Selectable>(out var selectable) || !_prioritizedSelectables.Contains(selectable)) continue;
            
            SetHovered(selectable);
            return;
        }

        SetHovered(filteredResults[0].collider.TryGetComponent<Selectable>(out var firstSelectable) ? firstSelectable : null);
    }

    private void SetHovered(Selectable selectable)
    {
        if (_disabled) return;

        if (selectable == _hovered) return;
        TryStopHover();
        _hovered = selectable;
        if (selectable == null) return;
        selectable.Hover();
        selectable.OnHoverStop.AddListener(HandleEarlyHoverStop);
    }

    private void TryStopHover()
    {
        if (_hovered == null) return;
        
        _hovered.OnHoverStop.RemoveListener(HandleEarlyHoverStop);
        _hovered.StopHover();
    }

    private void HandleEarlyHoverStop(Selectable selectable)
    {
        selectable.OnHoverStop.RemoveListener(HandleEarlyHoverStop);
        if (_hovered == selectable) _hovered = null;
    }

    public void SelectHovered()
    {
        if (_disabled) return;

        TryDeselect();
        if (_hovered != null)
        {
            _hovered.OnDeselect.AddListener(HandleEarlyDeselect);
            _hovered.Select();
        }
        _selected = _hovered;
    }

    private void TryDeselect()
    {
        if (_selected == null) return;
       
        _selected.OnDeselect.RemoveListener(HandleEarlyDeselect);
        _selected.Deselect();
    }

    private void HandleEarlyDeselect(Selectable selectable)
    {
        selectable.OnDeselect.RemoveListener(HandleEarlyDeselect);
        if (_selected == selectable) _selected = null;
    }

    public void EngageHovered()
    {
        if (_disabled) return;

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
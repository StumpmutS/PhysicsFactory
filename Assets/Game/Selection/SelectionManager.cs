using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;

public class SelectionManager : Singleton<SelectionManager>
{
    [SerializeField] private SelectionHoverManager hoverManager;
    
    private Selectable _selected;
    private HashSet<Selectable> _engaged = new();

    protected override void Awake()
    {
        base.Awake();
        SelectionDisabler.OnDisable += HandleSelectionDisabled;
    }

    private void HandleSelectionDisabled()
    {
        TryDeselect();
        DisengageAll();
    }

    public void SelectHovered()
    {
        if (SelectionDisabler.Disabled) return;

        TryDeselect();
        if (hoverManager.Hovered != null)
        {
            hoverManager.Hovered.OnDeselect.AddListener(HandleEarlyDeselect);
            hoverManager.Hovered.Select();
        }
        _selected = hoverManager.Hovered;
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
        if (SelectionDisabler.Disabled) return;

        if (hoverManager.Hovered != null)
        {
            if (!_engaged.Add(hoverManager.Hovered))
            {
                _engaged.Remove(hoverManager.Hovered);
                hoverManager.Hovered.OnDisengage.RemoveListener(HandleEarlyDisengage);
                hoverManager.Hovered.Disengage();
                return;
            }
            hoverManager.Hovered.Engage();
            hoverManager.Hovered.OnDisengage.AddListener(HandleEarlyDisengage);
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

    private void OnDestroy()
    {
        SelectionDisabler.OnDisable -= HandleSelectionDisabled;
    }
}
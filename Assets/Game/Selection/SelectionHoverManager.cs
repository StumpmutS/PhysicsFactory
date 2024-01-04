using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Scripts;

public class SelectionHoverManager : MonoBehaviour
{
    [SerializeField] private float maxSearchDistance = 200f;
    [SerializeField] private LayerMask selectableLayer;
    
    public Selectable Hovered { get; private set; }
    private HashSet<Selectable> _prioritizedSelectables = new();

    private void Awake()
    {
        SelectionDisabler.OnDisable += HandleSelectionDisabled;
    }

    private void HandleSelectionDisabled()
    {
        TryStopHover();
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

    private static readonly RaycastHit[] RaycastResults = new RaycastHit[128];
    
    private void UpdateHovered()
    {
        var found = Physics.RaycastNonAlloc(MainCameraRef.Cam.ScreenPointToRay(Input.mousePosition), RaycastResults, maxSearchDistance, selectableLayer);
        if (found == 0)
        {
            SetHovered(null);
            return;
        }
        
        var filteredResults = RaycastResults.Take(found)
            .OrderBy(hit => Vector3.SqrMagnitude(hit.point - MainCameraRef.Cam.transform.position)).ToArray();

        foreach (var hit in filteredResults)
        {
            if (!hit.collider.TryGetComponent<Selectable>(out var selectable) ||
                !_prioritizedSelectables.Contains(selectable)) continue;

            SetHovered(selectable);
            return;
        }

        SetHovered(filteredResults[0].collider.GetComponent<Selectable>());
    }

    private void SetHovered(Selectable selectable)
    {
        if (SelectionDisabler.Disabled) return;

        if (selectable == Hovered) return;
        TryStopHover();
        Hovered = selectable;
        if (selectable == null) return;
        selectable.Hover();
        selectable.OnHoverStop.AddListener(HandleEarlyHoverStop);
    }

    private void TryStopHover()
    {
        if (Hovered == null) return;
        
        Hovered.OnHoverStop.RemoveListener(HandleEarlyHoverStop);
        Hovered.StopHover();
    }

    private void HandleEarlyHoverStop(Selectable selectable)
    {
        selectable.OnHoverStop.RemoveListener(HandleEarlyHoverStop);
        if (Hovered == selectable) Hovered = null;
    }

    private void OnDestroy()
    {
        SelectionDisabler.OnDisable -= HandleSelectionDisabled;
    }
}
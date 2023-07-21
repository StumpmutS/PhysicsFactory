using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Scripts;
using Object = UnityEngine.Object;

public class Builder
{
    private GridProjector _gridProjector;
    private BuildingInfo _info;
    private BuildingPreview _mainPreview;
    private Stack<Vector3> _selectedLocations = new();
    private Stack<Vector3> _restrictedAxes = new();

    public Builder(GridProjector gridProjector, BuildingInfo info)
    {
        _gridProjector = gridProjector;
        _info = info;
        _mainPreview = Object.Instantiate(_info.PreviewPrefab);
        _mainPreview.gameObject.SetActive(false);
        _restrictedAxes.Push(Vector3.zero);
        
        _gridProjector.OnCellHovered += HandleCellHovered;
        _gridProjector.OnCellSelected += HandleCellSelected;
        InputTranslationManager.Instance.OnResetDown.AddListener(HandleReset);
    }

    public event Action OnBuildComplete = delegate { };

    private void HandleCellHovered(Cell3D cell)
    {
        var position = RoundAxes(cell.GetPosition(), out _);
        var hoveredLocations = new List<Vector3>(_selectedLocations.Reverse())
        {
            position
        };
        _mainPreview.gameObject.SetActive(true);
        _mainPreview.StretchTo(hoveredLocations, cell.Info.Size);
        UpdateRestrictions();
    }

    private void HandleCellSelected(Cell3D cell)
    {
        var position = RoundAxes(cell.GetPosition(), out var newRestriction);
        _selectedLocations.Push(position);
        _restrictedAxes.Push(newRestriction);
        UpdateRestrictions();
        
        if (_selectedLocations.Count >= _info.AnchorCellAmount) CompleteBuild();
    }

    private void HandleReset()
    {
        if (_selectedLocations.Count > 0) _selectedLocations.Pop();
        if (_restrictedAxes.Count > 1) _restrictedAxes.Pop();
    }

    private Vector3 RoundAxes(Vector3 pos, out Vector3 newRestriction)
    {
        newRestriction = default;
        if (!_info.RestrictToAxes || _selectedLocations.Count < 1) return pos;
        
        var restrictedDirection = (pos - _selectedLocations.Peek()).RoundToAxis(_restrictedAxes.Peek(), Vector3.up);
        newRestriction = _restrictedAxes.Peek() + restrictedDirection;
        return _selectedLocations.Peek() + restrictedDirection;
    }
    
    private void UpdateRestrictions()
    {
        if (RestrictionHelper.CheckRestrictions(_info.PlacementRestrictions, GenerateRestrictionInfo()))
        {
            _mainPreview.Pass();
            return;
        }

        _mainPreview.Deny();
    }

    private PlacementRestrictionInfo GenerateRestrictionInfo() => new (_mainPreview, _info.Price);

    public void Destroy()
    {
        _gridProjector.OnCellHovered -= HandleCellHovered;
        _gridProjector.OnCellSelected -= HandleCellSelected;
        InputTranslationManager.Instance.OnResetDown.RemoveListener(HandleReset);
        Object.Destroy(_mainPreview.gameObject);
    }

    private void CompleteBuild()
    {
        if (RestrictionHelper.TryPassRestrictions(_info.PlacementRestrictions, GenerateRestrictionInfo()))
        {
            _mainPreview.Place(_info);
        }
        OnBuildComplete.Invoke();
    }
}
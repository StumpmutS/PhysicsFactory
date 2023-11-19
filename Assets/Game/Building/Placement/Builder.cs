using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Scripts;
using Object = UnityEngine.Object;

public class Builder
{
    private Grid3D _grid;
    private BuildingInfo _info;
    private BuildingPreview _mainPreview;
    private Stack<Vector3> _selectedLocations = new();
    private Stack<Vector3> _restrictedAxes = new();

    public Builder(Grid3D grid, BuildingInfo info)
    {
        _grid = grid;
        _info = info;
        _mainPreview = Object.Instantiate(_info.PreviewPrefab);
        _mainPreview.gameObject.SetActive(false);
        _restrictedAxes.Push(Vector3.zero);
        
        InputTranslationManager.Instance.OnResetDown.AddListener(HandleReset);
        InputTranslationManager.Instance.OnInteractNonUIUp.AddListener(HandleInteract);
    }

    public event Action OnBuildComplete = delegate { };
    public event Action<RestrictionFailureInfo> OnBuildFailure = delegate { };

    public void Tick()
    {
        var position = RoundAxes(_grid.GetIntersectedPosition(MainCameraRef.Cam.ScreenPointToRay(Input.mousePosition),
                    YLevelManager.Instance.YLevel), out _);
        var hoveredLocations = new List<Vector3>(_selectedLocations.Reverse())
        {
            position
        };
        _mainPreview.gameObject.SetActive(true);
        _mainPreview.StretchTo(hoveredLocations, _grid.CellSize);
        UpdateRestrictions();
    }

    private void HandleInteract()
    {
        var position = RoundAxes(_grid.GetIntersectedPosition(MainCameraRef.Cam.ScreenPointToRay(Input.mousePosition),
            YLevelManager.Instance.YLevel), out var newRestriction);
        _selectedLocations.Push(position);
        _restrictedAxes.Push(newRestriction);
        UpdateRestrictions();

        if (_selectedLocations.Count < _info.AnchorCellAmount) return;
        if (TryCompleteBuild()) return;

        _selectedLocations.Pop();
        _restrictedAxes.Pop();
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
        if (RestrictionHelper.CheckRestrictions(_info.PlacementRestrictions, GenerateRestrictionInfo(), new RestrictionFailureInfo()))
        {
            _mainPreview.Pass();
            return;
        }

        _mainPreview.Deny();
    }

    private PlacementRestrictionInfo GenerateRestrictionInfo() => new (_mainPreview, _info.Price);

    public void Destroy()
    {
        InputTranslationManager.Instance.OnResetDown.RemoveListener(HandleReset);
        InputTranslationManager.Instance.OnInteractNonUIUp.RemoveListener(HandleInteract);
        Object.Destroy(_mainPreview.gameObject);
    }

    private bool TryCompleteBuild()
    {
        var failureInfo = new RestrictionFailureInfo();
        if (!RestrictionHelper.TryPassRestrictions(_info.PlacementRestrictions, GenerateRestrictionInfo(), failureInfo))
        {
            OnBuildFailure.Invoke(failureInfo);
            return false;
        }
        
        _mainPreview.Place(_info);
        OnBuildComplete.Invoke();
        return true;
    }
}
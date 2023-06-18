using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

public class Builder
{
    private GridProjector _gridProjector;
    protected BuildingInfo _info;
    protected BuildingPreview _mainPreview;
    protected List<Cell3D> _selectedCells = new();

    public Builder(GridProjector gridProjector, BuildingInfo info)
    {
        _gridProjector = gridProjector;
        _info = info;
        _mainPreview = Object.Instantiate(_info.PreviewPrefab);
        _mainPreview.gameObject.SetActive(false);
        
        _gridProjector.OnCellHovered += HandleCellHovered;
        _gridProjector.OnCellUnHovered += HandleCellUnHovered;
        _gridProjector.OnCellSelected += HandleCellSelected;
        _gridProjector.OnCellDeselected += HandleCellDeselected;
    }

    public event Action OnBuildComplete = delegate { };

    private void HandleCellHovered(Cell3D cell) 
    { 
        var destination = cell.GetPosition();
        _mainPreview.gameObject.SetActive(true);
        UpdateRestrictions();
        
        if (_selectedCells.Count < 1)
        {
            _mainPreview.transform.position = destination;
            return;
        }
        
        _mainPreview.StretchTo(_selectedCells[0].GetPosition(), destination, cell.Info.Size);
    }
    
    private void HandleCellUnHovered(Cell3D cell) { }

    private void HandleCellSelected(Cell3D cell)
    {
        _selectedCells.Add(cell);
        
        if (_selectedCells.Count < _info.AnchorCellAmount)
        {
            UpdateRestrictions();
            return;
        }

        if (RestrictionHelper.CheckRestrictions(_info.Restrictions, GenerateRestrictionInfo()))
        {
            CompleteBuild();
        }
    }
    
    private void HandleCellDeselected(Cell3D cell) { }

    private void UpdateRestrictions()
    {
        if (RestrictionHelper.CheckRestrictions(_info.Restrictions, GenerateRestrictionInfo()))
        {
            _mainPreview.Pass();
            return;
        }

        _mainPreview.Deny();
    }

    private BuildingRestrictionInfo GenerateRestrictionInfo()
    {
        return new BuildingRestrictionInfo(_mainPreview);
    }

    public void Destroy()
    {
        _gridProjector.OnCellHovered -= HandleCellHovered;
        _gridProjector.OnCellUnHovered -= HandleCellUnHovered;
        _gridProjector.OnCellSelected -= HandleCellSelected;
        _gridProjector.OnCellDeselected -= HandleCellDeselected;
        
        Object.Destroy(_mainPreview.gameObject);
    }

    private void CompleteBuild()
    {
        if (RestrictionHelper.TryPassRestrictions(_info.Restrictions, GenerateRestrictionInfo()))
        {
            _mainPreview.Place();
        }
        OnBuildComplete.Invoke();
    }
}
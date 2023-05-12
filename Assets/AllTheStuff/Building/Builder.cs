using System;
using System.Collections.Generic;
using System.Numerics;
using Object = UnityEngine.Object;

public class Builder
{
    private Grid3D _grid;
    protected BuildingInfo _info;
    protected BuildingPreview _mainPreview;
    protected List<Cell3D> _selectedCells = new();

    protected Builder(Grid3D grid, BuildingInfo info)
    {
        _grid = grid;
        _info = info;
        
        _grid.OnCellHovered += HandleCellHovered;
        _grid.OnCellUnHovered += HandleCellUnHovered;
        _grid.OnCellSelected += HandleCellSelected;
        _grid.OnCellDeselected += HandleCellDeselected;
    }

    public event Action<List<Cell3D>> OnBuildComplete = delegate { };

    protected virtual void HandleCellHovered(Cell3D cell) { }
    
    protected virtual void HandleCellUnHovered(Cell3D cell) { }

    protected virtual void HandleCellSelected(Cell3D cell) { }
    
    protected virtual void HandleCellDeselected(Cell3D cell) { }

    public void Destroy()
    {
        _grid.OnCellHovered -= HandleCellHovered;
        _grid.OnCellUnHovered -= HandleCellUnHovered;
        _grid.OnCellSelected -= HandleCellSelected;
        _grid.OnCellDeselected -= HandleCellDeselected;
        
        Object.Destroy(_mainPreview.gameObject);
    }

    protected void CompleteBuild()
    {
        OnBuildComplete.Invoke(_selectedCells);
    }
}
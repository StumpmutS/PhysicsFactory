using UnityEngine;

public class ChainBuilder : Builder
{
    public ChainBuilder(Grid3D grid, BuildingInfo info) : base(grid, info)
    {
        _mainPreview = Object.Instantiate(_info.PreviewPrefab);
        _mainPreview.gameObject.SetActive(false);
    }
    
    protected override void HandleCellHovered(Cell3D cell)
    {
        var destination = PositionFromCell(cell);
        _mainPreview.gameObject.SetActive(true);
        
        if (_selectedCells.Count < 1)
        {
            _mainPreview.transform.position = destination;
            return;
        }
        
        _mainPreview.StretchTo(PositionFromCell(_selectedCells[0]), destination);
    }

    protected override void HandleCellSelected(Cell3D cell)
    {
        _selectedCells.Add(cell);
        
        if (_selectedCells.Count < 2)
        {
            return;
        }
        
        CompleteBuild();
    }

    private Vector3 PositionFromCell(Cell3D cell)
    {
        return new Vector3(cell.Info.Center.x, cell.Info.Center.y - (float) cell.Info.Size / 2, cell.Info.Center.z);
    }
}
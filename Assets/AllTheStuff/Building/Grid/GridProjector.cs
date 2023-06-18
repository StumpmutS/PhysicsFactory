using System;
using System.Collections.Generic;
using UnityEngine;

public class GridProjector : MonoBehaviour
{
    [SerializeField] private Grid3D grid;
    [SerializeField] private Cell3D cellPrefab;
    
    public List<Cell3D> ProjectedCells { get; } = new();
    private bool _projected;

    public event Action<Cell3D> OnCellHovered = delegate { };
    public event Action<Cell3D> OnCellUnHovered = delegate { };
    public event Action<Cell3D> OnCellSelected = delegate { };
    public event Action<Cell3D> OnCellDeselected = delegate { };

    public void Project2DGrid(int yIndex)
    {
        if (_projected) UnProject2DGrid();
        _projected = true;
        
        yIndex = Mathf.Clamp(yIndex, 0, Mathf.FloorToInt(grid.Dimensions.y / grid.CellSize));

        for (int xIndex = 0; xIndex < grid.Grid.Count; xIndex++)
        {
            for (int zIndex = 0; zIndex < grid.Grid[xIndex][yIndex].Count; zIndex++)
            {
                var cell = CreateCell(xIndex, yIndex, zIndex);
                
                ProjectedCells.Add(cell);
            }
        }
    }

    private Cell3D CreateCell(int xIndex, int yIndex, int zIndex)
    {
        var cell = Instantiate(cellPrefab, grid.Grid[xIndex][yIndex][zIndex].Center, Quaternion.identity);
        cell.Init(grid.Grid[xIndex][yIndex][zIndex]);
        cell.OnHover += HandleCellHovered;
        cell.OnUnHover += HandleCellUnHovered;
        cell.OnSelect += HandleCellSelected;
        cell.OnDeselect += HandleCellDeselected;
        return cell;
    }

    private void DestroyCell(Cell3D cell)
    {
        cell.OnHover -= HandleCellHovered;
        cell.OnUnHover -= HandleCellUnHovered;
        cell.OnSelect -= HandleCellSelected;
        cell.OnDeselect -= HandleCellDeselected;
        Destroy(cell.gameObject);
    }
    
    private void HandleCellHovered(Cell3D cell) => OnCellHovered.Invoke(cell);
    
    private void HandleCellUnHovered(Cell3D cell) => OnCellUnHovered.Invoke(cell);

    private void HandleCellSelected(Cell3D cell) => OnCellSelected.Invoke(cell);

    private void HandleCellDeselected(Cell3D cell) => OnCellDeselected.Invoke(cell);

    public void UnProject2DGrid()
    {
        foreach (var cell in ProjectedCells)
        {
            DestroyCell(cell);
        }
        
        ProjectedCells.Clear();
    }
}
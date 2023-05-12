using System;
using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;

public class Grid3D : MonoBehaviour
{
    [SerializeField] private Vector3 origin;
    [SerializeField] private Vector3 dimensions;
    [SerializeField] private int cellSize;
    [SerializeField] private Cell3D cellPrefab;
    
    public List<Cell3D> ProjectedCells { get; } = new();

    private List<List<List<Cell3DInfo>>> _grid; //x then z is base, y is up
    private bool _projected;

    public event Action<Cell3D> OnCellHovered = delegate { };
    public event Action<Cell3D> OnCellUnHovered = delegate { };
    public event Action<Cell3D> OnCellSelected = delegate { };
    public event Action<Cell3D> OnCellDeselected = delegate { };

    private void Awake()
    {
        PopulateGrid();
    }

    private void PopulateGrid()
    {
        _grid = new List<List<List<Cell3DInfo>>>();
        for (int xIndex = 0; xIndex < Mathf.FloorToInt(dimensions.x / cellSize); xIndex++)
        {
            Vector3 centerReference = Vector3.zero;
            
            centerReference.x = (-(dimensions.x / 2) + xIndex + .5f) * cellSize + origin.x;

            var yList = new List<List<Cell3DInfo>>();
            for (int yIndex = 0; yIndex < Mathf.FloorToInt(dimensions.y / cellSize); yIndex++)
            {
                centerReference.y = (-(dimensions.y / 2) + yIndex + .5f) * cellSize + origin.y;
                
                var zList = new List<Cell3DInfo>();
                for (int zIndex = 0; zIndex < Mathf.FloorToInt(dimensions.z / cellSize); zIndex++)
                {
                    centerReference.z = (-(dimensions.z / 2) + zIndex + .5f) * cellSize + origin.z;

                    zList.Add(new Cell3DInfo(xIndex, yIndex, zIndex, new Vector3(centerReference.x, centerReference.y, centerReference.z), cellSize));
                }
                yList.Add(zList);
            }
            _grid.Add(yList);
        }
    }

    public void Project2DGrid(int yIndex)
    {
        if (_projected) UnProject2DGrid();
        _projected = true;
        
        yIndex = Mathf.Clamp(yIndex, 0, Mathf.FloorToInt(dimensions.x / cellSize));

        for (int xIndex = 0; xIndex < _grid.Count; xIndex++)
        {
            for (int zIndex = 0; zIndex < _grid[xIndex][yIndex].Count; zIndex++)
            {
                var cell = CreateCell(xIndex, yIndex, zIndex);
                
                ProjectedCells.Add(cell);
            }
        }
    }

    private Cell3D CreateCell(int xIndex, int yIndex, int zIndex)
    {
        var cell = Instantiate(cellPrefab, _grid[xIndex][yIndex][zIndex].Center, Quaternion.identity);
        cell.Init(_grid[xIndex][yIndex][zIndex]);
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
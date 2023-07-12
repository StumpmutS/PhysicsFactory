using System;
using System.Collections.Generic;
using UnityEngine;

public class GridProjector : MonoBehaviour
{
    [SerializeField] private Grid3D grid;
    [SerializeField] private YLevelManager yLevelManager;
    [SerializeField] private Cell3D cellPrefab;
    
    public List<List<Cell3D>> Cells { get; } = new();

    public event Action<Cell3D> OnCellHovered = delegate { };
    public event Action<Cell3D> OnCellUnHovered = delegate { };
    public event Action<Cell3D> OnCellSelected = delegate { };
    public event Action<Cell3D> OnCellDeselected = delegate { };

    private void Start()
    {
        for (int x = 0; x < grid.Dimensions.x; x++)
        {
            var xList = new List<Cell3D>();
            Cells.Add(xList);
            for (int z = 0; z < grid.Dimensions.z; z++)
            {
                var cell = Instantiate(cellPrefab);
                cell.Init(grid.Grid[x][yLevelManager.YLevel][z]);
                cell.OnHover += HandleCellHovered;
                cell.OnUnHover += HandleCellUnHovered;
                cell.OnSelect += HandleCellSelected;
                cell.OnDeselect += HandleCellDeselected;
                xList.Add(cell);
                cell.gameObject.SetActive(false);
            }
        }
    }

    public void Project2DGrid(int yIndex)
    {
        foreach (var cellList in Cells)
        {
            foreach (var cell in cellList)
            {
                SetCell(cell, yIndex);
                ProjectCell(cell);
            }
        }
    }

    public void UnProject2DGrid()
    {
        foreach (var cellList in Cells)
        {
            foreach (var cell in cellList)
            {
                UnProjectCell(cell);
            }
        }
    }

    private void ProjectCell(Cell3D cell)
    {
        cell.gameObject.SetActive(true);
    }

    private void SetCell(Cell3D cell, int yIndex)
    {
        var xIndex = cell.Info.XIndex;
        var zIndex = cell.Info.ZIndex;
        cell.transform.position = grid.Grid[xIndex][yIndex][zIndex].Center;
        cell.Init(grid.Grid[xIndex][yIndex][zIndex]);
    }

    private void UnProjectCell(Cell3D cell)
    {
        cell.gameObject.SetActive(false);
    }
    
    private void HandleCellHovered(Cell3D cell) => OnCellHovered.Invoke(cell);
    
    private void HandleCellUnHovered(Cell3D cell) => OnCellUnHovered.Invoke(cell);

    private void HandleCellSelected(Cell3D cell) => OnCellSelected.Invoke(cell);

    private void HandleCellDeselected(Cell3D cell) => OnCellDeselected.Invoke(cell);
}
using System.Collections.Generic;
using UnityEngine;

public class GridProjector : MonoBehaviour
{
    [SerializeField] private Grid3D grid;
    [SerializeField] private YLevelManager yLevelManager;
    [SerializeField] private Cell3D cellPrefab;
    
    private List<List<Cell3D>> _cells = new();

    private bool _projected;

    private void Start()
    {
        for (int x = 0; x < grid.Dimensions.x; x++)
        {
            var xList = new List<Cell3D>();
            _cells.Add(xList);
            for (int z = 0; z < grid.Dimensions.z; z++)
            {
                var cell = Instantiate(cellPrefab);
                cell.Init(grid.Grid[x][yLevelManager.YLevel][z]);
                xList.Add(cell);
                cell.gameObject.SetActive(false);
            }
        }
    }

    public void Project2DGrid(int yIndex = -1)
    {
        _projected = true;
        
        if (yIndex < 0) yIndex = yLevelManager.YLevel;
        
        foreach (var cellList in _cells)
        {
            foreach (var cell in cellList)
            {
                SetCell(cell, yIndex);
                ProjectCell(cell);
            }
        }
    }

    public void Refresh2DGrid(int yIndex = -1)
    {
        if (yIndex < 0) yIndex = yLevelManager.YLevel;

        if (_projected) Project2DGrid(yIndex);
    }

    public void UnProject2DGrid()
    {
        _projected = false;
        
        foreach (var cellList in _cells)
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
        var xIndex = cell.Data.XIndex;
        var zIndex = cell.Data.ZIndex;
        cell.transform.position = grid.Grid[xIndex][yIndex][zIndex].Center;
        cell.Init(grid.Grid[xIndex][yIndex][zIndex]);
    }

    private void UnProjectCell(Cell3D cell)
    {
        cell.gameObject.SetActive(false);
    }
}
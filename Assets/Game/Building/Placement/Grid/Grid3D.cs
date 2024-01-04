using System.Collections.Generic;
using UnityEngine;

public class Grid3D : MonoBehaviour
{
    [SerializeField] private Vector3 dimensions;
    public Vector3 Dimensions => dimensions;
    public Vector3 Extents => Dimensions / 2;
    
    public List<List<List<Cell3DData>>> Grid { get; private set; } //x then z is base, y is up

    
    private void Awake()
    {
        PopulateGrid();
    }

    private void PopulateGrid()
    {
        Grid = new List<List<List<Cell3DData>>>();
        for (int xIndex = 0; xIndex < Mathf.FloorToInt(dimensions.x); xIndex++)
        {
            Vector3 centerReference = Vector3.zero;
            
            centerReference.x = (-Extents.x + xIndex + .5f);

            var yList = new List<List<Cell3DData>>();
            for (int yIndex = 0; yIndex < Mathf.FloorToInt(dimensions.y); yIndex++)
            {
                centerReference.y = (-Extents.y + yIndex + .5f);
                
                var zList = new List<Cell3DData>();
                for (int zIndex = 0; zIndex < Mathf.FloorToInt(dimensions.z); zIndex++)
                {
                    centerReference.z = (-Extents.z + zIndex + .5f);

                    zList.Add(new Cell3DData(xIndex, yIndex, zIndex, new Vector3(centerReference.x, centerReference.y, centerReference.z)));
                }
                yList.Add(zList);
            }
            Grid.Add(yList);
        }
    }

    public bool GetIntersectedCellPosition(Ray ray, int yIndex, out Vector3 position)
    {
        position = default;
        if ((ray.origin.y < yIndex - Extents.y && ray.direction.y <= 0) ||
            (ray.origin.y > yIndex - Extents.y && ray.direction.y >= 0)) return false;
        
        var originDistanceFromIndex = ray.origin.y + Extents.y - yIndex;
        var result = (ray.origin + ray.direction * Mathf.Abs(originDistanceFromIndex / ray.direction.y));
        if (!PointInsideGrid(result)) return false; 
        
        position = new Vector3(ConvertToCellPosition(result.x), ConvertToCellPosition(result.y), ConvertToCellPosition(result.z));
        return true;
    }

    private bool PointInsideGrid(Vector3 point)
    {
        var x = point.x <= Extents.x && point.x >= -Extents.x;
        var y = point.y <= Extents.y && point.y >= -Extents.y;
        var z = point.z <= Extents.z && point.z >= -Extents.z;
        return x && y && z;
    }
    
    private float ConvertToCellPosition(float number)
    {
        number += .0001f; //Favor positive when whole number, important for negative y levels
        return number - number % 1 + Mathf.Sign(number) * .5f;
    }

    public Cell3DData GetCell(Vector3Int cellIndex)
    {
        return Grid[cellIndex.x + (int) Extents.x][cellIndex.y + (int) Extents.y][cellIndex.z + (int) Extents.z];
    }
}
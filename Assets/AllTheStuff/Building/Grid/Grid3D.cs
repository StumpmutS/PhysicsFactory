using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Scripts;

public class Grid3D : MonoBehaviour
{
    [SerializeField] private Vector3 origin;
    public Vector3 Origin => origin;
    [SerializeField] private Vector3 dimensions;
    public Vector3 Dimensions => dimensions;
    [SerializeField] private int cellSize;
    public int CellSize => cellSize;

    public List<List<List<Cell3DInfo>>> Grid { get; private set; } //x then z is base, y is up

    private void Awake()
    {
        PopulateGrid();
    }

    private void PopulateGrid()
    {
        Grid = new List<List<List<Cell3DInfo>>>();
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
            Grid.Add(yList);
        }
    }
}
using UnityEngine;

public class Cell3DInfo
{
    public bool Occupied;
    public int XIndex { get; }
    public int YIndex { get; }
    public int ZIndex { get; }
    public Vector3 Center { get; }
    public int Size { get; }

    public Cell3DInfo(int xIndex, int yIndex, int zIndex, Vector3 center, int size)
    {
        XIndex = xIndex;
        YIndex = yIndex;
        ZIndex = zIndex;
        Center = center;
        Size = size;
    }
}
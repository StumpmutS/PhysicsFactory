using UnityEngine;

public class Cell3DData
{
    public int XIndex { get; }
    public int YIndex { get; }
    public int ZIndex { get; }
    public Vector3 Center { get; }

    public Cell3DData(int xIndex, int yIndex, int zIndex, Vector3 center)
    {
        XIndex = xIndex;
        YIndex = yIndex;
        ZIndex = zIndex;
        Center = center;
    }
}
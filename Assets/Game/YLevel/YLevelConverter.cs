using UnityEngine;
using Utility.Scripts;

public class YLevelConverter : Singleton<YLevelConverter>
{
    [SerializeField] private Grid3D grid;

    public float YLevelToWorld(int yLevel = -1)
    {
        if (yLevel < 0) yLevel = YLevelManager.Instance.YLevel;
        return yLevel - (int) (grid.Dimensions.y / 2f * grid.CellSize + grid.CellSize / 2f);
    }

    public void WorldMinMax(int minYLevel, int maxYLevel, out float min, out float max)
    {
        min = YLevelToWorld(minYLevel);
        max = YLevelToWorld(maxYLevel);
    }
}
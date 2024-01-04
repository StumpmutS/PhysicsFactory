using UnityEngine;
using Utility.Scripts;

public class YLevelConverter : Singleton<YLevelConverter>
{
    [SerializeField] private Grid3D grid;

    public float YLevelToWorld(int yLevel = -1)
    {
        if (yLevel < 0) yLevel = YLevelManager.Instance.YLevel;
        return yLevel - (int) (grid.Extents.y + .5f);
    }

    public void WorldMinMax(int minYLevel, int maxYLevel, out float min, out float max)
    {
        min = YLevelToWorld(minYLevel);
        max = YLevelToWorld(maxYLevel);
    }
}
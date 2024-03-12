using UnityEngine;
using Utility.Scripts;

public class NodeFinderRangeUpgradeListener : UpgradeListener
{
    [SerializeField] private EnergyNodeFinder nodeFinder;
    [SerializeField] private SerializableDictionary<int, float> levelRanges;
    
    protected override void HandleUpgradeLevel(int level)
    {
        nodeFinder.MaxRange = levelRanges[level];
    }
}
using UnityEngine;
using Utility.Scripts.Extensions;

[CreateAssetMenu(menuName = "Placement/Processors/Axis Restriction")]
public class AxisRestrictionPlacementProcessor : PlacementProcessor
{
    [SerializeField] private bool excludeUsedAxes;
    
    public override void Process(PlacementProcessingData data)
    {
        if (data.SelectedPositions.Count < 1) return;

        var lastPosition = data.SelectedPositions.Peek();
        var proposed = data.Position - lastPosition;
        var isolated = proposed.IsolateAxis(GetExcludedAxes(data), Vector3.up);
        data.Position = lastPosition + isolated;
    }

    private Vector3 GetExcludedAxes(PlacementProcessingData data)
    {
        var excludedAxes = Vector3.zero;
        if (excludeUsedAxes || data.SelectedPositions.Count < 2) return excludedAxes;

        var next = data.SelectedPositions.Peek();
        foreach (var position in data.SelectedPositions)
        {
            excludedAxes += (next - position).IsolateAxis().normalized;
            next = position;
        }

        return excludedAxes;
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Scripts.Extensions;

[CreateAssetMenu(menuName = "Placement/Processors/Dynamic Dimension Stretch")]
public class DynamicDimensionStretchPlacementProcessor : PlacementProcessor
{
    [SerializeField] private Vector3 defaultDirection = Vector3.forward;
    
    public override void Process(PlacementProcessingData data)
    {
        var positions = new List<Vector3>(data.SelectedPositions.Reverse())
        {
            data.Position
        };
        
        Stretch(data.Preview.transform, positions);
    }

    private void Stretch(Transform transform, List<Vector3> positions)
    {
        if (positions.Count < 1 || positions.Count > 4) return;
        
        transform.position = positions[0];
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.LookRotation(positions.Count > 1 ? positions[1] - positions[0] : defaultDirection);

        for (int i = positions.Count - 1; i > 0; i--)
        {
            var direction = positions[i] - positions[i - 1];
            transform.localScale += (transform.rotation * direction).Abs();
            transform.position += direction / 2;
        }
    }
}
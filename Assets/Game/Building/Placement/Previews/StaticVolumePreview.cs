using System.Collections.Generic;
using UnityEngine;

public class StaticVolumePreview : BuildingPreview
{
    [SerializeField] private Vector3 dimensions;
    [SerializeField] private Vector3 defaultDirection;

    public override float Volume => dimensions.x * dimensions.y * dimensions.z;

    public override void StretchTo(List<Vector3> locations, int cellSize)
    {
        switch (locations.Count)
        {
            case 1: 
                OneLocation(locations, cellSize);
                break;
            case 2: 
                TwoLocations(locations, cellSize);
                break;
            default:
                Debug.LogWarning("Number of hovered builder locations did not match preview parameters");
                return;
        }
    }

    private void OneLocation(List<Vector3> locations, int cellSize)
    {
        transform.forward = defaultDirection;
        Offset(locations[0], cellSize);
    }

    private void TwoLocations(List<Vector3> locations, int cellSize)
    {
        var direction = locations[1] - locations[0];
        direction.y = 0;
        if (direction.sqrMagnitude == 0) transform.forward = defaultDirection;
        else transform.right = -direction;
        Offset(locations[0], cellSize);
    }

    private void Offset(Vector3 pos, int cellSize)
    {
        var offset = -dimensions / 2 + Vector3.one * (cellSize / 2f);
        offset.y *= -1;
        transform.position = pos;
        transform.Translate(offset, Space.Self);
    }
}
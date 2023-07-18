using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;

public class DynamicVolumePreview : BuildingPreview
{
    public override float Volume
    {
        get
        {
            var result = 1f;
            if (_locations.Count >= 2) result *= (_locations[1] - _locations[0]).magnitude + _cellSize;
            if (_locations.Count >= 3) result *= (_locations[2] - _locations[1]).magnitude + _cellSize;
            if (_locations.Count >= 4) result *= (_locations[3] - _locations[2]).magnitude + _cellSize;
            return result;
        }
    }

    private List<Vector3> _locations = new();
    private int _cellSize;

    public override void StretchTo(List<Vector3> locations, int cellSize)
    {
        _locations = new List<Vector3>(locations);
        _cellSize = cellSize;
        
        switch (locations.Count)
        {
            case 1: 
                OneLocation(locations);
                break;
            case 2: 
                TwoLocations(locations, cellSize);
                break;
            case 3: 
                ThreeLocations(locations, cellSize);
                break;
            case 4: 
                FourLocations(locations, cellSize);
                break;
            default:
                Debug.LogWarning("Number of hovered builder locations did not match preview parameters");
                return;
        }
    }

    private void OneLocation(List<Vector3> locations)
    {
        transform.position = locations[0];
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;
    }

    private void TwoLocations(List<Vector3> locations, int cellSize)
    {
        var direction = locations[1] - locations[0];
        transform.localScale = new Vector3(1, 1, direction.magnitude + cellSize);
        transform.forward = direction;
        transform.position = Vector3.Lerp(locations[1], locations[0], .5f);
    }

    private void ThreeLocations(List<Vector3> locations, int cellSize)
    {
        TwoLocations(locations, cellSize);
        var direction = locations[2] - locations[1];
        transform.localScale += (transform.rotation * direction).Abs();
        transform.position += direction / 2;
    }

    private void FourLocations(List<Vector3> locations, int cellSize)
    {
        ThreeLocations(locations, cellSize);
        var direction = locations[3] - locations[2];
        transform.localScale += (transform.rotation * direction).Abs();
        transform.position += direction / 2;
    }
}
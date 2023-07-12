using System.Collections.Generic;
using UnityEngine;

public class ChainPreview : BuildingPreview
{
    public override float Volume
    {
        get
        {
            if (_locations.Count != 2) return 1;
            return (_locations[1] - _locations[0]).magnitude + _cellSize;
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

    public override void Place()
    {
        var building = Instantiate(buildingPrefab, transform.position, transform.rotation);
        building.transform.localScale = transform.localScale;
    }
}
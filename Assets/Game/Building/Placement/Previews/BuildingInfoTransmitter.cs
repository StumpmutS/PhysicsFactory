using System;
using UnityEngine;

public class BuildingInfoTransmitter : MonoBehaviour
{
    [SerializeField] private Building building;
    
    public void Init(PlacedBuildingInfo info, Transform transformToCopy)
    {
        transform.position = transformToCopy.position;
        transform.rotation = transformToCopy.rotation;
        building.Init(info, transformToCopy);
    }
}
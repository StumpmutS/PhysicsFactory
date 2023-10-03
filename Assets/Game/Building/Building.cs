﻿using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    public PlacedBuildingInfo Info { get; private set; }

    public void Init(PlacedBuildingInfo info, Transform transformToCopy)
    {
        Info = info;
        transform.localScale = transformToCopy.localScale;
    }
    
    private void Start()
    {
        BuildingManager.Instance.AddBuilding(this);
    }

    private void OnDestroy()
    {
        BuildingManager.Instance.RemoveBuilding(this);
    }
}
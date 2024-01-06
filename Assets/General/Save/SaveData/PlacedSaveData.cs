using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class PlacedSaveData
{
    [SerializeField] private PlacedData data;
    public PlacedData Data => data;

    public PlacedSaveData(PlacedData data)
    {
        this.data = data;
    }
}
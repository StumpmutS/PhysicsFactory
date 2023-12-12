using UnityEngine;
using Utility.Scripts;

public class LayerManager : Singleton<LayerManager>
{
    [SerializeField] private LayerMask dolboidLayer;
    public LayerMask DolboidLayer => dolboidLayer;
    [SerializeField] private LayerMask buildingLayer;
    public LayerMask BuildingLayer => buildingLayer;
}
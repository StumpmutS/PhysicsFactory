using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingPreview : MonoBehaviour
{
    [SerializeField] protected BuildingInfoTransmitter buildingPrefab;
#pragma warning disable CS0108, CS0114
    [SerializeField] private Renderer renderer;
#pragma warning restore CS0108, CS0114
    [SerializeField] private ColorInfo previewColors;

    public abstract float Volume { get; }

    public abstract void StretchTo(List<Vector3> locations, int cellSize);
    
    public void Place(BuildingInfo info)
    {
        var building = Instantiate(buildingPrefab);
        building.Init(new PlacedBuildingInfo(info.Label, Volume, info.Price, info.SaleRestrictions), transform);
    }
    
    public void Pass()
    {
        SetMeshColor(previewColors.Colors[0]);
    }

    public void Deny()
    {
        SetMeshColor(previewColors.Colors[1]);
    }

    private void SetMeshColor(Color color)
    {
        var mats = renderer.materials;
        mats[0].color = color;
        renderer.materials = mats;
    }
}
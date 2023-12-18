using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;

public abstract class BuildingPreview : MonoBehaviour
{
    [SerializeField] protected BuildingInfoTransmitter buildingPrefab;
#pragma warning disable CS0108, CS0114
    [SerializeField] private List<PreviewRendererInfo> rendererInfo;
#pragma warning restore CS0108, CS0114
    [SerializeField] private ColorData previewColors;

    public abstract float Volume { get; }

    public abstract void StretchTo(List<Vector3> locations, int cellSize);
    
    public void Place(BuildingPlacementInfo info)
    {
        var building = Instantiate(buildingPrefab);
        building.Init(new PlacedBuildingData(info, Volume, new TransformData(transform)));
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
        float originalAlpha = color.a;
        foreach (var info in rendererInfo)
        {
            var mats = info.Renderer.materials;
            color.a = info.OverrideAlpha ? info.Alpha : originalAlpha;
            mats[0].color = color;
            info.Renderer.materials = mats;
        }
    }
}
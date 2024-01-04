using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;

public class BuildingPreview : MonoBehaviour
{
    [SerializeField] protected BuildingInfoTransmitter buildingPrefab;
    [SerializeField] private List<PreviewRendererInfo> rendererInfo;
    [SerializeField] private ColorData previewColors;

    public float Volume => transform.localScale.x * transform.localScale.y * transform.localScale.z;

    public void Place(BuildingPlacementData data)
    {
        var building = Instantiate(buildingPrefab);
        building.Init(new PlacedBuildingData(data, Volume, new TransformData(transform)));
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
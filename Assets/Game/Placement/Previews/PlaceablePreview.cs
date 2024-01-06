using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Utility.Scripts;

public class PlaceablePreview : MonoBehaviour
{
    [FormerlySerializedAs("buildingPrefab")] [SerializeField] protected PlaceableTransmitter placeablePrefab;
    [SerializeField] private List<PreviewRendererData> rendererInfo;
    [SerializeField] private ColorData previewColors;

    public float Volume => transform.localScale.x * transform.localScale.y * transform.localScale.z;

    public void Place(PlacementData data)
    {
        var transmitter = Instantiate(placeablePrefab);
        transmitter.Init(new PlacedData(data, Volume, new TransformData(transform)));
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
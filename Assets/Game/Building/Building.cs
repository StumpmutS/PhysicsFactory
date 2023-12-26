using System;
using UnityEngine;

public class Building : MonoBehaviour, ISaveable<BuildingSaveData>
{
    [SerializeField] private IdentifiableObject identifiableObject;
    
    public PlacedBuildingData Data { get; private set; }

    public void Init(PlacedBuildingData data)
    {
        Data = data;
        transform.localScale = data.TransformData.LocalScale;
    }

    private void Start()
    {
        BuildingManager.Instance.AddBuilding(this);
    }

    private void OnDestroy()
    {
        BuildingManager.Instance.RemoveBuilding(this);
    }

    public void Save(BuildingSaveData data, AssetRefCollection assetRefCollection)
    {
        data.PlacedBuildingSaveData = new PlacedBuildingSaveData(Data);
        data.IdentifiableObjectSaveData ??= new IdentifiableObjectSaveData(-1);
        identifiableObject.Save(data.IdentifiableObjectSaveData, assetRefCollection);
    }
}
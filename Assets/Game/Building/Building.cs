using System;
using UnityEngine;
using UnityEngine.Events;

public class Building : MonoBehaviour, ISaveable<BuildingSaveData>
{
    [SerializeField] private IdentifiableObject identifiableObject;
    
    public PlacedBuildingData Data { get; private set; }

    public UnityEvent OnInitialized = new();

    public void Init(PlacedBuildingData data)
    {
        Data = data;
        transform.localScale = data.TransformData.LocalScale;
        OnInitialized.Invoke();
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
using System;
using UnityEngine;
using Utility.Scripts;

[Serializable]
public class PlacedData
{
    [SerializeField] private ContextData contextData;
    public ContextData ContextData => contextData;
    [SerializeField] private float volume;
    public float Volume => volume;
    [SerializeField] private float price;
    public float Price => price;
    [SerializeField] private float saleMultiplier;
    public float SaleMultiplier => saleMultiplier;
    [SerializeField] private TransformData transformData;
    public TransformData TransformData => transformData;
    
    public PlacedData(ContextData contextData, float volume, float price, float saleMultiplier, TransformData transformData)
    {
        this.contextData = contextData;
        this.volume = volume;
        this.price = price;
        this.saleMultiplier = saleMultiplier;
        this.transformData = transformData;
    }
    
    public PlacedData(PlacementData data, float volume, TransformData transformData)
        : this(data.Context, volume, data.Price, data.SaleMultiplier, transformData) { }
}
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;
using Utility.Scripts;

[Serializable]
public class PlacedData
{
    [SerializeField] private string label;
    public string Label => label;
    [SerializeField] private float volume;
    public float Volume => volume;
    [SerializeField] private float price;
    public float Price => price;
    [SerializeField] private float saleMultiplier;
    public float SaleMultiplier => saleMultiplier;
    [SerializeField] private TransformData transformData;
    public TransformData TransformData => transformData;
    
    public PlacedData(string label, float volume, float price, float saleMultiplier, TransformData transformData)
    {
        this.label = label;
        this.volume = volume;
        this.price = price;
        this.saleMultiplier = saleMultiplier;
        this.transformData = transformData;
    }
    
    public PlacedData(PlacementData data, float volume, TransformData transformData)
        : this(data.Label, volume, data.Price, data.SaleMultiplier, transformData) { }
}
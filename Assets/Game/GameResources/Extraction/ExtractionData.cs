using System;
using UnityEngine;

[Serializable]
public class ExtractionData
{
    [SerializeField] private Resource prefab;
    public Resource Prefab => prefab;
    [SerializeField] private ResourceData data;
    public ResourceData Data => data;
}
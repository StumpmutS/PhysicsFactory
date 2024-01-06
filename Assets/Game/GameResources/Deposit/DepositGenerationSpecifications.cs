using System.Collections.Generic;
using UnityEngine;

public class DepositGenerationSpecifications : MonoBehaviour
{
    [SerializeField] private RuntimeDepositGenerator generator;
    [SerializeField] private List<DepositGenerationData> generationData;

    private void Start()
    {
        foreach (var data in generationData)
        {
            generator.Generate(data);
        }
    }
}
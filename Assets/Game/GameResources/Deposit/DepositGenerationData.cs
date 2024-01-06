using System;
using UnityEngine;

[Serializable]
public class DepositGenerationData
{
    [SerializeField] private Vector3Int maxDimensions;
    public Vector3Int MaxDimensions => maxDimensions;
    [SerializeField] private Vector3Int startCell;
    public Vector3Int StartCell => startCell;
    [SerializeField, Range(0, 1)] private float openingChance;
    public float OpeningChance => openingChance;
    [SerializeField] private int minimumOpenings;
    public int MinimumOpenings => minimumOpenings;
}
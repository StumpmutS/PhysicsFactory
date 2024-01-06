using System;
using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;
using Utility.Scripts.Extensions;
using Random = UnityEngine.Random;

public class RuntimeDepositGenerator : MonoBehaviour
{
    [SerializeField] private Grid3D grid;
    [SerializeField] private GameObject depositPrefab;
    [SerializeField] private GameObject staticWallPrefab;

    private List<List<List<EDepositSlot>>> _gridSlots = new();

    public void Generate(DepositGenerationData data)
    {
        _gridSlots.Clear();
        _gridSlots.Populate(() => new List<EDepositSlot>(), data.MaxDimensions.x, data.MaxDimensions.y);
        foreach (var yList in _gridSlots)
        {
            yList.Populate(EDepositSlot.None, data.MaxDimensions.y, data.MaxDimensions.z);
        }
        SetGridSlots(data.MaxDimensions, data.MinimumOpenings, data.OpeningChance);
        CreateDeposit(data.MaxDimensions, data.StartCell);
    }

    private void SetGridSlots(Vector3Int dimensions, int minOpenings, float openingChance)
    {
        var guaranteedOpenings = DetermineGuaranteedOpenings(dimensions, minOpenings);

        for (int x = 0; x < dimensions.x; x++)
        {
            for (int y = 0; y < dimensions.y; y++)
            {
                for (int z = 0; z < dimensions.z; z++)
                {
                    if (guaranteedOpenings.Contains(new Vector3Int(x, y, z)))
                    {
                        _gridSlots[x][y][z] = EDepositSlot.None;
                        continue;
                    }
                    
                    var xEdge = x == 0 || x == Mathf.RoundToInt(dimensions.x - 1);
                    var yEdge = y == 0 || y == Mathf.RoundToInt(dimensions.y - 1);
                    var zEdge = z == 0 || z == Mathf.RoundToInt(dimensions.z - 1);
                    var edgeNumber = Convert.ToInt32(xEdge) + Convert.ToInt32(yEdge) + Convert.ToInt32(zEdge);
                    _gridSlots[x][y][z] = edgeNumber switch
                    {
                        0 => EDepositSlot.Deposit,
                        > 1 => EDepositSlot.Wall,
                        _ => Random.Range(0f, 1f) <= openingChance ? EDepositSlot.None : EDepositSlot.Wall
                    };
                }
            }
        }
    }

    private HashSet<Vector3Int> DetermineGuaranteedOpenings(Vector3Int dimensions, int minOpenings)
    {
        var openings = new HashSet<Vector3Int>();
        int possible = (dimensions.x - 2) * (dimensions.y - 2) + (dimensions.x - 2) * (dimensions.z - 2) +
                       (dimensions.y - 2) * (dimensions.z - 2);
        if (possible < minOpenings)
        {
            Debug.LogError($"Requested minimum openings ({minOpenings}) exceeds available openings ({possible})");
            return openings;
        }

        while (openings.Count < minOpenings)
        {
            var opening = StumpRandom.DeterminePrismSingleEdgeOpening(dimensions);
            openings.Add(opening);
        }

        return openings;
    }

    private void CreateDeposit(Vector3Int dimensions, Vector3Int cellIndex)
    {
        for (int x = 0; x < dimensions.x; x++)
        {
            for (int y = 0; y < dimensions.y; y++)
            {
                for (int z = 0; z < dimensions.z; z++)
                {
                    var pos = grid.GetCell(cellIndex + new Vector3Int(x, y, z)).Center;
                    if (_gridSlots[x][y][z] == EDepositSlot.Deposit)
                    {
                        Instantiate(depositPrefab, pos, Quaternion.identity);
                    }
                    if (_gridSlots[x][y][z] == EDepositSlot.Wall)
                    {
                        Instantiate(staticWallPrefab, pos, Quaternion.identity);
                    }
                }
            }
        }
    }

    private enum EDepositSlot
    {
        None,
        Deposit,
        Wall
    }
}
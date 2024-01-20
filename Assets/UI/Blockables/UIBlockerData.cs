using System;
using System.Linq;
using UnityEngine;

public class UIBlockerData
{
    public GameObject BlockingPrefab;
    public UIBlockableData UIBlockableData;
    
    public UIBlockerData(GameObject blockingPrefab, UIBlockableData uiBlockableData)
    {
        BlockingPrefab = blockingPrefab;
        UIBlockableData = uiBlockableData;
    }
}
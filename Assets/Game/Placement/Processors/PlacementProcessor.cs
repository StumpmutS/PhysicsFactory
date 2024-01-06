using UnityEngine;

public abstract class PlacementProcessor : ScriptableObject
{
    public abstract void Process(PlacementProcessingData data);
}
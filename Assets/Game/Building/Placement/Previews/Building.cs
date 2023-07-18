using UnityEngine;

public class Building : MonoBehaviour
{
    public PlacedBuildingInfo Info { get; private set; }
    
    public void Init(PlacedBuildingInfo info)
    {
        Info = info;
    }
}
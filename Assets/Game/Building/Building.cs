using UnityEngine;

public class Building : MonoBehaviour
{
    public PlacedBuildingData Data { get; private set; }

    public void Init(PlacedBuildingData data)
    {
        Data = data;
        transform.localScale = data.TransformData.LocalScale;
    }
    
    private void Start()
    {
        BuildingManager.Instance.AddBuilding(this);
    }

    private void OnDestroy()
    {
        BuildingManager.Instance.RemoveBuilding(this);
    }
}
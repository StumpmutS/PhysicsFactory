using UnityEngine;

public class Building : MonoBehaviour
{
    public PlacedBuildingInfo Info { get; private set; }

    public void Init(PlacedBuildingInfo info)
    {
        Info = info;
        transform.localScale = info.TransformData.LocalScale;
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
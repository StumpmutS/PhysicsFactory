using UnityEngine;

public class BuildingRefDisplayToggle : DisplayCallbackToggle<PlaceableBuildingDisplayData>
{
    [SerializeField] private Label label;
    
    protected override void DisplayData(PlaceableBuildingDisplayData displayData)
    {
        label.SetLabel(displayData.BuildingContainer.Asset.Data.Label);
    }
}
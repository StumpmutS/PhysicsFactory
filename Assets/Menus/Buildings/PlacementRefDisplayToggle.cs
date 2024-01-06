using UnityEngine;

public class PlacementRefDisplayToggle : DisplayCallbackToggle<PlacementDisplayData>
{
    [SerializeField] private Label label;
    
    protected override void DisplayData(PlacementDisplayData displayData)
    {
        label.SetLabel(displayData.PlacementContainer.Asset.Data.Label);
    }
}
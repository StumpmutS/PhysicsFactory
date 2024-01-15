using UnityEngine;

public class PlacementRefDisplayToggle : DisplayCallbackToggle<PlacementDisplayData>
{
    [SerializeField] private ContextDataContainer contextContainer;
    
    protected override void DisplayData(PlacementDisplayData displayData)
    {
        contextContainer.SetData(displayData.PlacementContainer.Asset.Data.Context);
    }
}
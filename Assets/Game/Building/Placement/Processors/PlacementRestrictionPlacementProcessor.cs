using UnityEngine;

[CreateAssetMenu(menuName = "Placement/Processors/Placement Restriction")]
public class PlacementRestrictionPlacementProcessor : PlacementProcessor
{
    public override void Process(PlacementProcessingData data)
    {
        if (RestrictionHelper.CheckRestrictions(data.PlacementRestrictions, data.InfoGetter.Invoke(), new RestrictionFailureInfo()))
        {
            data.Preview.Pass();
            return;
        }

        data.Preview.Deny();
    }
}
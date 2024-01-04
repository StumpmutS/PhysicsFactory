using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Placement/Extraction Adjacent")]
public class PlacementExtractionAdjacentRestriction : Restriction<PlacementRestrictionInfo>
{
    protected override ERestrictionFailureType RestrictionFailureType => ERestrictionFailureType.ExtractionAdjacent;
    
    protected override bool Check(PlacementRestrictionInfo restrictionInfo, RestrictionFailureInfo failureInfo)
    {
        throw new System.NotImplementedException();
    }
}
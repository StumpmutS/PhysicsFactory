using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Placement/Building Collision")]
public class PlacementCollisionRestriction : Restriction<PlacementRestrictionInfo>
{
    protected override ERestrictionFailureType RestrictionFailureType => ERestrictionFailureType.PlacementCollision;

    protected override bool Check(PlacementRestrictionInfo restrictionInfo, RestrictionFailureInfo failureInfo)
    {
        var results = new Collider[4];
        Physics.OverlapBoxNonAlloc(restrictionInfo.Preview.transform.position,
            restrictionInfo.Preview.transform.localScale / 2 - Vector3.one * .005f, results,
            restrictionInfo.Preview.transform.rotation, LayerManager.Instance.BuildingLayer);
        return results.All(c => c == null);
    }
}
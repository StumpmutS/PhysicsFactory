using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Placement/Building Collision")]
public class PlacementCollisionRestriction : Restriction<PlacementRestrictionInfo>
{
    protected override ERestrictionFailureType RestrictionFailureType => ERestrictionFailureType.PlacementCollision;

    private static readonly Collider[] Colliders = new Collider[1];

    protected override bool Check(PlacementRestrictionInfo restrictionInfo, RestrictionFailureInfo failureInfo)
    {
        return Physics.OverlapBoxNonAlloc(restrictionInfo.Preview.transform.position,
            restrictionInfo.Preview.transform.localScale / 2 - Vector3.one * .005f, Colliders,
            restrictionInfo.Preview.transform.rotation, LayerManager.Instance.BuildingLayer) < 1;
    }
}
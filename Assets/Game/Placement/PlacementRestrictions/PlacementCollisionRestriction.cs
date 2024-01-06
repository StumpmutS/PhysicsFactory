using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Placement/Collision")]
public class PlacementCollisionRestriction : Restriction<PlacementRestrictionInfo>
{
    [SerializeField] private LayerMask collisionLayer;
    
    protected override ERestrictionFailureType RestrictionFailureType => ERestrictionFailureType.PlacementCollision;

    private static readonly Collider[] Colliders = new Collider[1];

    protected override bool Check(PlacementRestrictionInfo restrictionInfo, RestrictionFailureInfo failureInfo)
    {
        return Physics.OverlapBoxNonAlloc(restrictionInfo.Preview.transform.position,
            restrictionInfo.Preview.transform.localScale / 2 - Vector3.one * .005f, Colliders,
            restrictionInfo.Preview.transform.rotation, collisionLayer) < 1;
    }
}
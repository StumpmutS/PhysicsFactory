using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Placement/Extraction Adjacent")]
public class PlacementExtractionAdjacentRestriction : Restriction<PlacementRestrictionInfo>
{
    [SerializeField] private LayerMask collisionLayer;
    
    protected override ERestrictionFailureType RestrictionFailureType => ERestrictionFailureType.Extraction;

    private static readonly Collider[] Colliders = new Collider[16];
    
    protected override bool Check(PlacementRestrictionInfo restrictionInfo, RestrictionFailureInfo failureInfo)
    {
        var previewTransform = restrictionInfo.Preview.transform;
        var found = Physics.OverlapBoxNonAlloc(previewTransform.position,
            previewTransform.localScale / 2 + Vector3.one * .1f,
            Colliders, Quaternion.identity, collisionLayer);

        for (int i = 0; i < found; i++)
        {
            if (Colliders[i].TryGetComponent<Extractable>(out _)) return true;
        }

        return false;
    }
}
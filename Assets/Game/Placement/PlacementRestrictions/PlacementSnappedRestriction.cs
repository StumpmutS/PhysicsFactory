using UnityEngine;
using Utility.Scripts;

[CreateAssetMenu(menuName = "Restrictions/Placement/Snapped")]
public class PlacementSnappedRestriction : Restriction<PlacementRestrictionInfo>
{
    [SerializeField] private ERestrictionFailureType restrictionFailureType;
    [SerializeField] private ESnapType snapType;
    [SerializeField] private LayerMask snapLayer;
    [SerializeField] private float maxSearchDistance = 200f;
    
    protected override ERestrictionFailureType RestrictionFailureType => restrictionFailureType;

    private static readonly RaycastHit[] Hits = new RaycastHit[256];
    
    protected override bool Check(PlacementRestrictionInfo restrictionInfo, RestrictionFailureInfo failureInfo)
    {
        var ray = MainCameraRef.Cam.ScreenPointToRay(Input.mousePosition);
        var found = Physics.RaycastNonAlloc(ray, Hits, maxSearchDistance, snapLayer);

        for (int i = 0; i < found; i++)
        {
            if (!Hits[i].collider.TryGetComponent<SnapTarget>(out var target) || !target.CanSnap(snapType)) continue;

            return true;
        }

        return false;
    }
}
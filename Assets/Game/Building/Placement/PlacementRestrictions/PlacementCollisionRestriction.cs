using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Placement/Building Collision")]
public class PlacementCollisionRestriction : Restriction<PlacementRestrictionInfo>
{
    public override bool CheckRestriction(PlacementRestrictionInfo info)
    {
        var results = new Collider[4];
        Physics.OverlapBoxNonAlloc(info.Preview.transform.position,
            info.Preview.transform.localScale / 2 - Vector3.one * .005f, results,
            info.Preview.transform.rotation, LayerManager.Instance.BuildingLayer);
        return results.All(c => c == null);
    }
}
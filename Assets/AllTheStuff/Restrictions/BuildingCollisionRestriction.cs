using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Building Collision")]
public class BuildingCollisionRestriction : BuildingRestriction
{
    public override bool CheckRestriction(BuildingRestrictionInfo info)
    {
        var results = new Collider[4];
        Physics.OverlapBoxNonAlloc(info.Preview.transform.position,
            info.Preview.transform.localScale / 2 - Vector3.one * .005f, results,
            info.Preview.transform.rotation, LayerManager.Instance.BuildingLayer);
        return results.All(c => c == null);
    }
}
using UnityEngine;
using Utility.Scripts;

[CreateAssetMenu(menuName = "Restrictions/GameObject/Layer")]
public class LayerRestriction : GameObjectRestriction
{
    [SerializeField] private LayerMask layerMask;
    
    protected override ERestrictionFailureType RestrictionFailureType => ERestrictionFailureType.None;
    
    protected override bool Check(GameObject go, RestrictionFailureInfo failureInfo)
    {
        return LayerHelper.LayerEqualsMask(go.layer, layerMask);
    }
}
using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/GameObject/Never")]
public class NeverRestriction : GameObjectRestriction
{
    protected override ERestrictionFailureType RestrictionFailureType => ERestrictionFailureType.None;
    
    protected override bool Check(GameObject _, RestrictionFailureInfo failureInfo)
    {
        return false;
    }
}
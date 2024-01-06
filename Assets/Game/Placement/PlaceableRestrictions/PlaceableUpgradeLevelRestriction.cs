using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Placeable/UpgradeLevel")]
public class PlaceableUpgradeLevelRestriction : Restriction<PlaceableRestrictionData>
{
    [SerializeField] private int levelRequirement;

    protected override ERestrictionFailureType RestrictionFailureType => ERestrictionFailureType.UpgradeLevel;

    protected override bool Check(PlaceableRestrictionData restrictionData, RestrictionFailureInfo failureInfo)
    {
        if (!restrictionData.Placeable.TryGetComponent<Upgradeable>(out var upgradeable)) return false;

        return upgradeable.Level >= levelRequirement;
    }
}
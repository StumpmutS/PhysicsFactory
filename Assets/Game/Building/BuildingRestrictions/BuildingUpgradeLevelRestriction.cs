using UnityEngine;

[CreateAssetMenu(menuName = "Restrictions/Building/UpgradeLevel")]
public class BuildingUpgradeLevelRestriction : Restriction<BuildingRestrictionInfo>
{
    [SerializeField] private int levelRequirement;

    protected override ERestrictionFailureType RestrictionFailureType => ERestrictionFailureType.UpgradeLevel;

    protected override bool Check(BuildingRestrictionInfo restrictionInfo, RestrictionFailureInfo failureInfo)
    {
        failureInfo.UpgradeLevel = levelRequirement;
        if (!restrictionInfo.Building.TryGetComponent<Upgradeable>(out var upgradeable)) return false;

        return upgradeable.Level >= levelRequirement;
    }
}
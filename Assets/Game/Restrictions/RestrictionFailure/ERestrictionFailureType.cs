using System;

[Flags]
public enum ERestrictionFailureType
{
    None = 0,
    Supply = 1,
    UpgradeLevel = 2,
    PlacementCollision = 4,
    Extraction = 8
}
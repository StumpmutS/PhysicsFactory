using UnityEngine;

public abstract class Modification : MonoBehaviour
{
    private ModificationInfo _modInfo;
    private ModificationRestrictionInfo _restrictionInfo;

    protected Transform _mainTransform;
    
    public void Init(ModificationInfo info, Transform mainTransform)
    {
        _modInfo = info;
        _mainTransform = mainTransform;
    }
    
    public bool TryActivate(Building building)
    {
        _restrictionInfo = GenerateRestrictionInfo(building);
        if (!RestrictionHelper.TryPassRestrictions(_modInfo.ActivationRestrictions, _restrictionInfo)) return false;
        Activate();
        return true;
    }
    
    protected abstract void Activate();

    public bool TryDeactivate()
    {
        if (!RestrictionHelper.TryPassRestrictions(_modInfo.SaleRestrictions, _restrictionInfo)) return false;
        Deactivate();
        return true;
    }

    protected abstract void Deactivate();

    private ModificationRestrictionInfo GenerateRestrictionInfo(Building building)
    {
        return new ModificationRestrictionInfo(building, _modInfo.Price, _modInfo.SaleMultiplier);
    }
}
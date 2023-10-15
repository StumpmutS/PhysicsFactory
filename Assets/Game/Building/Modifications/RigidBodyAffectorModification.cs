using UnityEngine;
using Utility.Scripts;

public class RigidBodyAffectorModification : ModificationComponent
{
    [SerializeField] private RigidBodyAffector rigidBodyAffectorPrefab;

    private RigidBodyAffectorContainer _container;
    private RigidBodyAffector _affector;
    private EnergySpreadController _spreadController;
    private IEnergySpender _spender;

    public override void Activate(Transform mainTransform)
    {
        _container = mainTransform.AddOrGetComponent<RigidBodyAffectorContainer>();
        _affector = _container.AddAffector(rigidBodyAffectorPrefab);
        if (_affector is not IEnergySpender spender ||
            !mainTransform.TryGetComponent<EnergySpreadController>(out var spreadController)) return;
        spreadController.RegisterSpender(spender);
        _spreadController = spreadController;
        _spender = spender;
    }

    public override void Deactivate()
    {
        if (_container != null) _container.RemoveAffector(_affector);
        if (_spreadController != null && _spender != null)
        {
            _spreadController.DeregisterSpender(_spender);
        }

        _container = null;
        _affector = null;
        _spreadController = null;
        _spender = null;
    }
}
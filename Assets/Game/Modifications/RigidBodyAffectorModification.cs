using UnityEngine;
using Utility.Scripts.Extensions;

public class RigidBodyAffectorModification : ModificationComponent
{
    [SerializeField] private RigidBodyAffector rigidBodyAffectorPrefab;

    private RigidBodyAffectorContainer _container;
    private RigidBodyAffector _affector;

    public override void Activate(Transform mainTransform)
    {
        _container = mainTransform.AddOrGetComponent<RigidBodyAffectorContainer>();
        _affector = _container.AddAffector(rigidBodyAffectorPrefab);
    }

    public override void Deactivate()
    {
        if (_container != null) _container.RemoveAffector(_affector);
    }
}
using UnityEngine;

public class PhysicMaterialModification : Modification
{
    [SerializeField] private PhysicMaterial material;

    private Collider _collider;
    private PhysicMaterial _previousMaterial;
    
    protected override void Activate()
    {
        if (!_mainTransform.TryGetComponent<Collider>(out var foundCollider)) return;

        _collider = foundCollider;
        _previousMaterial = foundCollider.material;
        Debug.Log(_previousMaterial);
        foundCollider.material = material;
    }

    protected override void Deactivate()
    {
        if (_previousMaterial == null || _collider == null) return;
        _collider.material = _previousMaterial;
    }
}
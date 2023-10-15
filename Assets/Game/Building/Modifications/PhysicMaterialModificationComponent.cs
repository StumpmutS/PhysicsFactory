using UnityEngine;

public class PhysicMaterialModificationComponent : ModificationComponent
{
    [SerializeField] private PhysicMaterial material;

    private Collider _collider;
    private PhysicMaterial _previousMaterial;
    
    public override void Activate(Transform mainTransform)
    {
        if (!mainTransform.TryGetComponent<Collider>(out var foundCollider)) return;

        _collider = foundCollider;
        _previousMaterial = foundCollider.material;
        Debug.Log(_previousMaterial);
        foundCollider.material = material;
    }

    public override void Deactivate()
    {
        if (_previousMaterial == null || _collider == null) return;
        _collider.material = _previousMaterial;
    }
}
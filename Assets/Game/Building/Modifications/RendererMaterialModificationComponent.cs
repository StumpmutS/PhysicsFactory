using UnityEngine;
using Utility.Scripts;

public class RendererMaterialModificationComponent : ModificationComponent
{
    [SerializeField] private Material swapMaterial;

    private MaterialManager _materialManager;
    private Material _defaultMaterial;
    
    public override void Activate(Transform mainTransform)
    {
        if (!mainTransform.TryGetComponentInChildren(out _materialManager)) return;
        
        _defaultMaterial = _materialManager.SwapMaterial(swapMaterial);
    }

    public override void Deactivate()
    {
        if (_materialManager != null) _materialManager.SwapMaterial(_defaultMaterial);
    }
}
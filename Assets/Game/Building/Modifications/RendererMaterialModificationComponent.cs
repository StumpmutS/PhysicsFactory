using UnityEngine;
using Utility.Scripts;

public class RendererMaterialModificationComponent : ModificationComponent
{
    [SerializeField] private Material swapMaterial;

    private Renderer _renderer;
    private Material _defaultMaterial;
    
    public override void Activate(Transform mainTransform)
    {
        if (!mainTransform.TryGetComponentInChildren(out _renderer)) return;
        
        _defaultMaterial = _renderer.material;
        _renderer.material = swapMaterial;
    }

    public override void Deactivate()
    {
        if (_renderer != null) _renderer.material = _defaultMaterial;
    }
}
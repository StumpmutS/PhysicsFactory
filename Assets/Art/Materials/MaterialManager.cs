using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MaterialManager : MonoBehaviour
{
#pragma warning disable CS0108, CS0114
    [FormerlySerializedAs("renderer")] [SerializeField] private Renderer primaryRenderer;
#pragma warning restore CS0108, CS0114
    [SerializeField] private List<Renderer> secondaryRenderers;
    [SerializeField] private MaterialPairs materialPairs;

    private Material _materialReference;
    private bool _zWrite;
    private Material _materialInstance;
    public Material MaterialInstance
    {
        get
        {
            if (_materialInstance == primaryRenderer.sharedMaterial) return _materialInstance;

            if (_materialInstance != null) Destroy(_materialInstance);
            _materialInstance = Instantiate(primaryRenderer.material);
            SetMaterial(_materialInstance);
            return _materialInstance;
        }
        private set => _materialInstance = value;
    }
    private Dictionary<int, Action<int, Material>> _materialActions = new();
    private Dictionary<int, Action<int, Material>> _persistentMaterialActions = new();
    private Dictionary<int, object> _persistentCallers = new();

    private void Awake()
    {
        _materialReference = primaryRenderer.sharedMaterial;
        if (MaterialHelper.IsZWriteMaterial(_materialReference, out var zWrite)) _zWrite = zWrite;
    }

    public void ModifyMaterial(int propertyId, Action<int, Material> materialAction)
    {
        _materialActions[propertyId] = materialAction;
        materialAction.Invoke(propertyId, MaterialInstance);
    }

    public void ModifyMaterialPersistent(int propertyId, Action<int, Material> materialAction, object caller)
    {
        _persistentMaterialActions[propertyId] = materialAction;
        _persistentCallers[propertyId] = caller;
        materialAction.Invoke(propertyId, MaterialInstance);
    }

    public void RemovePersistentModification(int propertyId, object caller)
    {
        if (!_persistentCallers.TryGetValue(propertyId, out var currentCaller) || currentCaller != caller) return;
        
        _persistentCallers.Remove(propertyId);
        _persistentMaterialActions.Remove(propertyId);
    }
    
    /// <summary>
    /// Swaps material, applying persistent modifiers and conditionally applying non-persistent modifiers
    /// </summary>
    /// <param name="material">Material to swap</param>
    /// <param name="checkZWrite">Will use correct Z Write Material variant if set to true</param>
    /// <returns>Material that has been swapped</returns>
    public Material SwapMaterial(Material material, bool checkZWrite = true)
    {
        if (material == _materialReference) return material;
        
        if (checkZWrite && MaterialHelper.IsZWriteMaterial(material, out var zWrite))
        {
            if (zWrite != _zWrite)
            {
                if (_zWrite) materialPairs.TryGetZWriteOnMaterial(material, out material);
                else materialPairs.TryGetZWriteOffMaterial(material, out material);
            }
        }
        
        var prevReference = _materialReference;
        _materialReference = material;
        SetMaterial(material);
        
        if (materialPairs.IsPair(prevReference, _materialReference))
        {
            InvokeActions(_materialActions);
        }
        else
        {
            _materialActions.Clear();
        }
        
        InvokeActions(_persistentMaterialActions);
        
        return prevReference;
    }

    private void InvokeActions(Dictionary<int, Action<int, Material>> actions)
    {
        foreach (var kvp in actions)
        {
            kvp.Value.Invoke(kvp.Key, MaterialInstance);
        }
    }

    private void SetMaterial(Material material)
    {
        primaryRenderer.material = material;
        foreach (var secondary in secondaryRenderers)
        {
            secondary.material = material;
        }
    }

    public void ZWriteOn()
    {
        if (_zWrite) return;
        _zWrite = true;
        
        if (materialPairs.TryGetZWriteOnMaterial(_materialReference, out var zOnMaterial))
        {
            SwapMaterial(zOnMaterial, checkZWrite: false);
        }
    }

    public void ZWriteOff()
    {
        if (!_zWrite) return;
        _zWrite = false;
        
        if (materialPairs.TryGetZWriteOffMaterial(_materialReference, out var zOffMaterial))
        {
            SwapMaterial(zOffMaterial, checkZWrite: false);
        }
    }

    private void OnDestroy()
    {
        if (_materialInstance != null) Destroy(_materialInstance);
    }
}
﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
#pragma warning disable CS0108, CS0114
    [SerializeField] private Renderer renderer;
#pragma warning restore CS0108, CS0114
    [SerializeField] private MaterialPairs materialPairs;

    private Material _materialReference;
    private bool _zWrite;
    private Material _materialInstance;
    private Material MaterialInstance
    {
        get
        {
            if (_materialInstance == renderer.sharedMaterial) return _materialInstance;

            if (_materialInstance != null) Destroy(_materialInstance);
            _materialInstance = Instantiate(renderer.material);
            renderer.material = _materialInstance;
            return _materialInstance;
        }
        set => _materialInstance = value;
    }
    private Dictionary<int, Action<int, Material>> _materialActions = new();

    private void Awake()
    {
        _materialReference = renderer.sharedMaterial;
        if (MaterialHelper.IsZWriteMaterial(_materialReference, out var zWrite)) _zWrite = zWrite;
    }

    public void ModifyMaterial(int propertyId, Action<int, Material> materialAction)
    {
        _materialActions[propertyId] = materialAction;
        materialAction.Invoke(propertyId, MaterialInstance);
    }
    
    public Material SwapMaterial(Material material, bool checkZWrite = true)
    {
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
        renderer.material = material;
        
        foreach (var actionKvp in _materialActions)
        {
            actionKvp.Value.Invoke(actionKvp.Key, MaterialInstance);
        }
        return prevReference;
    }

    public void ZWriteOn()
    {
        if (_zWrite) return;
        _zWrite = true;
        
        if (materialPairs.TryGetZWriteOnMaterial(_materialReference, out var zOnMaterial))
        {
            SwapMaterial(zOnMaterial, false);
        }
    }

    public void ZWriteOff()
    {
        if (!_zWrite) return;
        _zWrite = false;
        
        if (materialPairs.TryGetZWriteOffMaterial(_materialReference, out var zOffMaterial))
        {
            SwapMaterial(zOffMaterial, false);
        }
    }

    private void OnDestroy()
    {
        if (_materialInstance != null) Destroy(_materialInstance);
    }
}